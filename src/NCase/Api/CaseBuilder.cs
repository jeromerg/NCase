using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Castle.DynamicProxy;
using NCase.Api.Dev;
using NVisitor.Api.Batch;
using NVisitor.Api.Marker;

namespace NCase.Api
{
    /// <summary>
    /// CaseBuilder is the main class you need to use NCase. 
    /// It provides the factory `TComponent CreateCaseComponent&lt;TComponent>(params object[] args)` to create the 
    /// case components and `TDirector VisitAst&lt;TDirector>()` to walk the case syntax tree with visitors.</summary>
    public class CaseBuilder 
    {
        private readonly IContainer mContainer;
        private readonly AstNode mAstNode = new AstNode();

        public CaseBuilder()
        {
            var builder = new ContainerBuilder();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            // register Singletons
            builder.RegisterInstance(new ProxyGenerator());

            // register factories of case-components (components contributing to defining of cases)
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => typeof (IContribFactory).IsAssignableFrom(t))
                .As<IContribFactory>()
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
        /// <typeparam name="TComponent">The type of component - core implementation currently only support interface</typeparam>
        /// <param name="args">Parameters to pass to the constructor of the component - currently not supported</param>
        /// <returns>a case component, that you can use to define the case tree</returns>
        public TComponent CreateCaseComponent<TComponent>(params object[] args)
        {
            return mContainer
                .Resolve<IEnumerable<IContribFactory>>()
                .First(f => f.CanHandle<TComponent>())
                .Create<TComponent>(mAstNode, args);
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
            director.Visit(mAstNode);
            return director;
        }

    }
}
