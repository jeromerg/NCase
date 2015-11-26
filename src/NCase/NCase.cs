using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using NDsl;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
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

            [NotNull]
            public TTool GetService<TTool>() where TTool : IService<T>
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                return mComponentContext.Resolve<TTool>();
            }
        }

        #endregion

        [NotNull]
        public static CaseBuilder NewBuilder()
        {
            var cb = new ContainerBuilder();

            cb.RegisterModule(new NDslCoreModule(new[] {Assembly.GetExecutingAssembly()}));
            cb.RegisterModule<NDslRecPlayModule>();
            cb.RegisterModule<NCaseCoreModule>();
            cb.RegisterModule<NCaseInterfaceRecPlayModule>();
            cb.RegisterModule<NCaseSeqModule>();
            cb.RegisterModule<NCaseTreeModule>();
            cb.RegisterModule<NCaseCombinationsModule>();
            cb.RegisterModule<NCaseProdModule>();
            cb.RegisterModule<NCasePairwiseModule>();

            cb.RegisterGeneric(typeof (Services<>)).As(typeof (IServiceSet<>));
            IContainer container = cb.Build();
            return container.Resolve<IBuilderFactory>().Create();
        }
    }
}