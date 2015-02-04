using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Castle.DynamicProxy;
using NCase.Api.Dev;
using NDsl.Api.Dev;
using NVisitor.Api.Batch;
using NVisitor.Api.Marker;

namespace NCase.Api
{
    /// <summary>
    /// Dsl is the entry point of NDsl framework. 
    /// It provides the factory `TComponent CreateContributor&lt;TComponent>(params object[] args)` to create the 
    /// case components and `TDirector VisitAst&lt;TDirector>()` to walk the case syntax tree with visitors.</summary>
    public class Dsl 
    {
        private readonly IContainer mContainer;
        private readonly RootNode mRootNode = new RootNode();

        public Dsl()
        {
            var builder = new ContainerBuilder();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            // register Singletons
            builder.RegisterInstance(new ProxyGenerator());

            // register factories of case-components (components contributing to defining of cases)
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => typeof (IContributorFactory).IsAssignableFrom(t))
                .As<IContributorFactory>()
                .PreserveExistingDefaults();

            // register visitor's directors. 
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => typeof(IDirectorMarker).IsAssignableFrom(t))
                .AsSelf()
                .InstancePerDependency(); // Directors are stateful

            // register visitors. 
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => typeof(IVisitorMarker).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .SingleInstance(); // Visitors are stateless

            mContainer = builder.Build();

        }

        /// <summary> Creates a case component, that you can use to define your cases </summary>
        /// <typeparam name="T">The type of component - core implementation currently only support interface</typeparam>
        /// <param name="args">Parameters to pass to the constructor of the component - currently not supported</param>
        /// <returns>a case component, that you can use to define the case tree</returns>
        public T CreateContributor<T>(params object[] args)
        {
            return mContainer
                .Resolve<IEnumerable<IContributorFactory>>()
                .First(f => f.CanHandle<T>())
                .Create<T>(mRootNode, args);
        }

        /// <summary>
        /// Visits the case tree. Use `DumpDirector` to dump the case tree definition into string. Use 
        /// `DevelopDirector` to produce all elementary cases.
        /// </summary>
        /// <typeparam name="TDir">the visitor's director that must visit the case tree definition</typeparam>
        /// <returns></returns>
        public TDir VisitAst<TDir>()
            where TDir : IDirector<INode, TDir>
        {
            TDir director = mContainer.Resolve<TDir>();
            director.Visit(mRootNode);
            return director;
        }

    }
}
