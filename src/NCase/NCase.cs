using System;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using NDsl;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Ui
{
    public static class NCase
    {
        #region inner types

        private class Services<T> : IServiceSet<T>
        {
            private readonly IComponentContext mComponentContext;

            public Services([NotNull] IComponentContext componentContext)
            {
                if (componentContext == null) throw new ArgumentNullException("componentContext");
                mComponentContext = componentContext;
            }

            public TTool GetService<TTool>() where TTool : IService<T>
            {
                return mComponentContext.Resolve<TTool>();
            }
        }

        #endregion

        public static IBuilder NewBuilder()
        {
            var cb = new ContainerBuilder();

            cb.RegisterModule(new NDslCoreModule(new[] {Assembly.GetExecutingAssembly()}));
            cb.RegisterModule<NDslRecPlayModule>();
            cb.RegisterModule<NCaseCoreModule>();
            cb.RegisterModule<NCaseInterfaceRecPlayModule>();
            cb.RegisterModule<NCaseSeqModule>();
            cb.RegisterModule<NCaseTreeModule>();
            cb.RegisterModule<NCaseProdModule>();
            cb.RegisterModule<NCasePairwiseModule>();
            cb.RegisterGeneric(typeof (Services<>)).As(typeof (IServiceSet<>));
            IContainer container = cb.Build();
            return container.Resolve<IBuilderFactory>().Create();
        }
    }
}