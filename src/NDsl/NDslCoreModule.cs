using System.Reflection;
using Autofac;
using NDsl.Api.Dev.Core.Util;
using NDsl.Imp.Core;
using NDsl.Imp.Core.Util;
using NVisitor.Api.Action;
using NVisitor.Api.ActionPair;
using Module = Autofac.Module;

namespace NDsl
{
    public class NDslCoreModule : Module
    {
        private readonly Assembly[] mNonUserAssemblies;

        /// <param name="nonUserAssemblies">
        /// Assemblies considered as framework assemblies. 
        /// They will be ignored while retrieving the user code location 
        /// of user statements</param>
        public NDslCoreModule(Assembly[] nonUserAssemblies)
        {
            mNonUserAssemblies = nonUserAssemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(new StackFrameUtil(mNonUserAssemblies)).As<IStackFrameUtil>();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
            builder.RegisterType<TokenStream>().AsImplementedInterfaces();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
            builder.RegisterGeneric(typeof(ActionVisitMapper<,>)).AsSelf().As(typeof(IActionVisitMapper<,>));
            builder.RegisterGeneric(typeof(ActionPairVisitMapper<,,>)).AsSelf().As(typeof(IActionPairVisitMapper<,,>));
        }
    }
}
