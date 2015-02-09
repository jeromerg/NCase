using Autofac;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NDsl.Impl.Core;
using NDsl.Impl.Core.Util;

namespace NDsl.Autofac
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<AstRoot>().As<IAstRoot>();
            builder.RegisterType<CodeLocationUtil>().As<ICodeLocationUtil>();
        }
    }
}
