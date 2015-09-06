using System.Reflection;
using Autofac;
using NDsl.Api.Core.Util;
using NDsl.Imp.Core;
using NDsl.Imp.Core.Util;
using NVisitor.Api.Batch;
using Module = Autofac.Module;

namespace NDsl.Autofac
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
            builder.RegisterGeneric(typeof(VisitMapper<,>)).AsSelf().As(typeof(IVisitMapper<,>));
        }
    }
}
