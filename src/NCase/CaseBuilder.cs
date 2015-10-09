using System;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using NDsl;

namespace NCase.Front.Ui
{
    public static class CaseBuilder
    {
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

            IContainer container = cb.Build();
            return container.Resolve<IBuilder>();
        }
    }
}