using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Features.Variance;
using Castle.DynamicProxy;
using NTestCase.Api.Dev;
using NTestCase.Base;
using NTestCase.BuilderStrategy;
using NTestCase.Util.Visit;

namespace NTestCase.Api.Use
{
    public class TestCaseBuilder : TestCaseBuilder<ImplicitBranchingBuilderStrategy>
    {
    }

    public class TestCaseBuilder<TStrategy> 
        where TStrategy : IBuilderStrategy
    {
        private readonly IContainer mContainer;
        private readonly INode<ITarget> mRootNode = new RootNode();

        public TestCaseBuilder()
        {
            var builder = new ContainerBuilder();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterInstance(new ProxyGenerator());

            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => typeof (IComponentFactory).IsAssignableFrom(t))
                .As<IComponentFactory>()
                .PreserveExistingDefaults();

            builder.RegisterType<TStrategy>()
                .As<IBuilderStrategy>();

            // register all visitor states and all visitors (a visitor always injects a state)
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => typeof (IDirector).IsAssignableFrom(t))
                .AsClosedTypesOf(typeof (IDirector< /*TNod*/>))
                .InstancePerDependency();

            // look for all visitor definitions
            // and register them, so that they can be injected into the directors
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => typeof (IVisitor).IsAssignableFrom(t))
                .AsClosedTypesOf(typeof (IVisitor</*TDir*/,/*TNod*/>));

            mContainer = builder.Build();

        }

        public T CreateComponent<T>(params object[] args)
        {
            var builderStrategy = mContainer.Resolve<IBuilderStrategy>();

            return mContainer
                .Resolve<IEnumerable<IComponentFactory>>()
                .First(f => f.CanHandle<T>())
                .Create<T>(mRootNode, builderStrategy, args);
        }

        public T VisitTestTree<T>()
            where T : IDirector<INode<ITarget>>
        {
            var director = mContainer.Resolve<T>();
            director.Visit(mRootNode);
            return director;
        }
    }
}
