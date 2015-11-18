using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
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
        private readonly Assembly[] mNonUserAssemblies;

        /// <param name="nonUserAssemblies">
        ///     Assemblies considered as framework assemblies.
        ///     They will be ignored while retrieving the user code location
        ///     of user statements
        /// </param>
        public NDslCoreModule(Assembly[] nonUserAssemblies)
        {
            mNonUserAssemblies = nonUserAssemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TableBuilder>().As<ITableBuilder>().InstancePerDependency();

            builder.RegisterInstance(new StackFrameUtil(mNonUserAssemblies)).As<IStackFrameUtil>();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
            builder.RegisterType<TokenStream>().AsImplementedInterfaces();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
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