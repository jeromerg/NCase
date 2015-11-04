using Autofac;
using Castle.DynamicProxy;
using NCaseFramework.Back.Imp.InterfaceRecPlay;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Imp.RecPlay;

namespace NDsl
{
    public class NDslRecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new ProxyGenerator());
            builder.RegisterType<InterfaceRecPlayNode.Factory>().As<IInterfaceReIInterfaceRecPlayNodeFactory>().SingleInstance();
            builder.RegisterType<InterfaceRecPlayContributorFactory>().As<IInterfaceRecPlayContributorFactory>().SingleInstance();
            builder.RegisterType<PrintCaseTableVisitors>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PrintCaseVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}