using Autofac;
using NCase.Back.Imp.RecPlay;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Imp.RecPlay;

namespace NDsl
{
    public class NDslRecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InterfaceRecPlayNode.Factory>().As<IInterfaceReIInterfaceRecPlayNodeFactory>().SingleInstance();
            builder.RegisterType<InterfaceRecPlayContributorFactory>().As<IInterfaceRecPlayContributorFactory>().SingleInstance();
            builder.RegisterType<PrintCaseTableVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}