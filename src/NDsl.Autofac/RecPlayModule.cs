using Autofac;
using Castle.DynamicProxy;

namespace NDsl.Autofac
{
    public class RecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreModule>();
            builder.RegisterInstance(new ProxyGenerator());
            builder.RegisterType<Impl.RecPlay.RecPlayContributorFactory>().As<Api.RecPlay.IRecPlayContributorFactory>();
            builder.RegisterType<Impl.RecPlay.RePlayDir>().As<Api.RecPlay.IRePlayDir>();
            builder.RegisterType<Impl.RecPlay.RecPlayInterfacePropertyNode>().As<Api.RecPlay.IRecPlayInterfacePropertyNode>();
            builder.RegisterType<Impl.RecPlay.DumpVisitors>().AsImplementedInterfaces();
            builder.RegisterType<Impl.RecPlay.PlayVisitors>().AsImplementedInterfaces();
        }
    }
}
