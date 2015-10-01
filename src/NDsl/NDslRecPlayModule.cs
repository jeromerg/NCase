using Autofac;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Imp.RecPlay;

namespace NDsl
{
    public class NDslRecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InterfaceRecPlayNode.Factory>().As<IInterfaceReIInterfaceRecPlayNodeFactory>();
            builder.RegisterType<InterfaceRecPlayContributorFactory>().As<IInterfaceRecPlayContributorFactory>();
            builder.RegisterType<InterfaceRecPlayNode>().As<IInterfaceRecPlayNode>();
        }
    }
}