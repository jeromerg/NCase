using Autofac;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Imp.RecPlay;

namespace NDsl
{
    public class NDslRecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InterfaceRecPlayContributorFactory>().As<IInterfaceRecPlayContributorFactory>();
            builder.RegisterType<InterfaceRecPlayNode>().As<IInterfaceRecPlayNode>();
            builder.RegisterType<DumpVisitors>().AsImplementedInterfaces();
        }
    }
}