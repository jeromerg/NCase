using Autofac;
using Castle.DynamicProxy;
using NDsl.Api.RecPlay;
using NDsl.Impl.RecPlay;

namespace NDsl.Autofac
{
    public class RecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreModule>();
            builder.RegisterInstance(new ProxyGenerator());
            builder.RegisterType<RecPlayContributorFactory>().As<IRecPlayContributorFactory>();
        }
    }
}
