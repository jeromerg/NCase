using Autofac;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NDsl.Impl.Core;
using NDsl.Impl.Core.Util;
using NVisitor.Api.Batch;

namespace NDsl.Autofac
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<StackFrameUtil>().As<IStackFrameUtil>();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
            builder.RegisterType<AstRoot>().As<IAstRoot>();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
            builder.RegisterGeneric(typeof(VisitMapper<,>)).AsSelf().As(typeof(IVisitMapper<,>));
        }
    }
}
