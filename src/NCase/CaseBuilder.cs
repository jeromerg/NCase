using System;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using NCase.Front.Api.Builder;
using NDsl;
using NDsl.Back.Api.Util;

namespace NCase.Front.Ui
{
    public static class CaseBuilder
    {
        #region inner types

        private class Services<T> : IServices<T>
        {
            private readonly IComponentContext mComponentContext;

            public Services([NotNull] IComponentContext componentContext)
            {
                if (componentContext == null) throw new ArgumentNullException("componentContext");
                mComponentContext = componentContext;
            }

            public TTool GetTool<TTool>() where TTool : IService<T>
            {
                return mComponentContext.Resolve<TTool>();
            }
        }

        #endregion

        public static IBuilder Create()
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
            cb.RegisterGeneric(typeof (Services<>)).As(typeof (IServices<>));
            IContainer container = cb.Build();
            return container.Resolve<IBuilderFactory>().Create();
        }
    }
}