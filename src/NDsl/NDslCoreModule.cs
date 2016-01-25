using System.Diagnostics.CodeAnalysis;
using Autofac;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;
using NDsl.Back.Api.Util.Table;
using NDsl.Back.Imp.Common;
using NDsl.Back.Imp.Util;
using NDsl.Back.Imp.Util.Table;
using NVisitor.Api.Action;
using NVisitor.Api.ActionPair;
using NVisitor.Api.ActionPayload;
using NVisitor.Api.ActionPayloadPair;
using NVisitor.Api.Func;
using NVisitor.Api.FuncPair;
using NVisitor.Api.FuncPayload;
using NVisitor.Api.FuncPayloadPair;
using Module = Autofac.Module;

namespace NDsl
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class NDslCoreModule : Module
    {
        [NotNull] private readonly IUserStackFrameUtil mUserStackFrameUtil;

        public NDslCoreModule([NotNull] IUserStackFrameUtil userStackFrameUtil)
        {
            mUserStackFrameUtil = userStackFrameUtil;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TableBuilder>().As<ITableBuilder>().InstancePerDependency();

            builder.RegisterInstance(mUserStackFrameUtil).As<IUserStackFrameUtil>();
            builder.RegisterType<CodeLocationPrinter>().As<ICodeLocationPrinter>();
            builder.RegisterType<TableBuilder.Factory>().As<ITableBuilderFactory>();
            builder.RegisterType<CodeLocationFactory>().As<ICodeLocationFactory>();
            builder.RegisterType<FileCache>().As<IFileCache>();
            builder.RegisterType<FileAnalyzer>().As<IFileAnalyzer>();
            builder.RegisterType<TokenStream>().AsImplementedInterfaces();
            builder.RegisterGeneric(typeof (ActionVisitMapper<,>)).AsSelf().As(typeof (IActionVisitMapper<,>));
            builder.RegisterGeneric(typeof (ActionPayloadVisitMapper<,,>)).AsSelf().As(typeof (IActionPayloadVisitMapper<,,>));
            builder.RegisterGeneric(typeof (ActionPairVisitMapper<,,>)).AsSelf().As(typeof (IActionPairVisitMapper<,,>));
            builder.RegisterGeneric(typeof (ActionPayloadPairVisitMapper<,,,>))
                   .AsSelf()
                   .As(typeof (IActionPayloadPairVisitMapper<,,,>));
            builder.RegisterGeneric(typeof (FuncVisitMapper<,,>)).AsSelf().As(typeof (IFuncVisitMapper<,,>));
            builder.RegisterGeneric(typeof (FuncPairVisitMapper<,,,>)).AsSelf().As(typeof (IFuncPairVisitMapper<,,,>));
            builder.RegisterGeneric(typeof (FuncPayloadVisitMapper<,,,>)).AsSelf().As(typeof (IFuncPayloadVisitMapper<,,,>));
            builder.RegisterGeneric(typeof (FuncPayloadPairVisitMapper<,,,,>))
                   .AsSelf()
                   .As(typeof (IFuncPayloadPairVisitMapper<,,,,>));
        }
    }
}