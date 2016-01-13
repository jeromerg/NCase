using System.Diagnostics.CodeAnalysis;
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
    public static class NCaseLegacy
    {
        [NotNull]
        public static CaseBuilder NewBuilder()
        {
            var cb = new ContainerBuilder();

            var userStackFrameUtil = new UserStackFrameUtil();
            cb.RegisterModule(new NDslCoreModule(userStackFrameUtil));
            cb.RegisterModule<NDslRecPlayModule>();
            cb.RegisterModule<NCaseCoreModule>();
            cb.RegisterModule<NCaseInterfaceRecPlayModule>();
            cb.RegisterModule<NCaseCombinationSetModule>();
            cb.RegisterGeneric(typeof (ServiceSet<>)).As(typeof (IServiceSet<>));

            cb.RegisterModule<NCaseSeqModule>();
            cb.RegisterModule<NCaseTreeModule>();
            cb.RegisterModule<NCaseProdModule>();
            cb.RegisterModule<NCasePairwiseModule>();

            IContainer container = cb.Build();
            return container.Resolve<IBuilderFactory>().Create();
        }
    }
}