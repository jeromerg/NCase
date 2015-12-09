using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using JetBrains.Annotations;
using NCaseFramework.Front.Api;
using NDsl;
using NDsl.Back.Api.Util;
using NDsl.Back.Imp.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public static class NCase
    {
        [NotNull]
        public static CaseBuilder NewBuilder()
        {
            var cb = new ContainerBuilder();

            var userUserStackFrameUtil = new UserUserStackFrameUtil(excludedAssemblies: new[] {Assembly.GetExecutingAssembly()});
            cb.RegisterModule(new NDslCoreModule(userUserStackFrameUtil));
            cb.RegisterModule<NDslRecPlayModule>();
            cb.RegisterModule<NCaseCoreModule>();
            cb.RegisterModule<NCaseInterfaceRecPlayModule>();
            cb.RegisterModule<NCaseSeqModule>();
            cb.RegisterModule<NCaseTreeModule>();
            cb.RegisterModule<NCaseCombinationsModule>();
            cb.RegisterModule<NCaseProdModule>();
            cb.RegisterModule<NCasePairwiseModule>();

            cb.RegisterGeneric(typeof (ServiceSet<>)).As(typeof (IServiceSet<>));
            IContainer container = cb.Build();
            return container.Resolve<IBuilderFactory>().Create();
        }
    }
}