using Autofac;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NDsl.Imp.Core;
using NDsl.Imp.Core.Util;
using NVisitor.Api.Batch;

namespace NDsl.Autofac
{
    public class NDslCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<StackFrameUtil>().As<IStackFrameUtil>();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
            builder.RegisterType<TokenStream>().AsImplementedInterfaces();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
            builder.RegisterGeneric(typeof(VisitMapper<,>)).AsSelf().As(typeof(IVisitMapper<,>));
        }
    }
}
