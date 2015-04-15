using Autofac;
using Castle.DynamicProxy;
using NDsl.Imp.RecPlay;

namespace NDsl.Autofac
{
    public class RecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreModule>();
            builder.RegisterInstance(new ProxyGenerator());
            builder.RegisterType<RecPlayContributorFactory>().As<Api.RecPlay.IRecPlayContributorFactory>();
            builder.RegisterType<RePlayDirector>().As<Api.RecPlay.IRePlayDirector>();
            builder.RegisterType<RecPlayInterfacePropertyNode>().As<Api.RecPlay.IRecPlayInterfacePropertyNode>();
            builder.RegisterType<DumpVisitors>().AsImplementedInterfaces();
            builder.RegisterType<PlayVisitors>().AsImplementedInterfaces();
        }
    }
}
