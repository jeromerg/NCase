using System;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using NDsl;
using NDsl.All;

namespace NCase.Front.Ui
{
    public static class CaseBuilder
    {
        #region inner types

        [UsedImplicitly]
        private class ToolBox : IToolBox
        {
            [NotNull] private readonly IComponentContext mComponentContext;

            public ToolBox([NotNull] IComponentContext componentContext)
            {
                if (componentContext == null) throw new ArgumentNullException("componentContext");
                mComponentContext = componentContext;
            }

            public T Resolve<T>()
            {
                return mComponentContext.Resolve<T>();
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

            cb.RegisterType<ToolBox>().As<IToolBox>();

            IContainer container = cb.Build();
            return container.Resolve<IBuilder>();
        }
    }
}