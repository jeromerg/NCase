using Autofac;
using Castle.DynamicProxy;
using NDsl.Api.RecPlay;
using NDsl.Imp.RecPlay;

namespace NDsl.Autofac
{
    public class NDslRecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<NDslCoreModule>();
            builder.RegisterInstance(new ProxyGenerator());
            builder.RegisterType<InterfaceRecPlayContributorFactory>().As<IInterfaceRecPlayContributorFactory>();
            builder.RegisterType<InterfaceRecPlayNode>().As<IInterfaceRecPlayNode>();
            builder.RegisterType<DumpVisitors>().AsImplementedInterfaces();
        }
    }
}
