using Autofac;
using NCase.Api.Nod;
using NCase.Imp;
using NCase.Imp.Nod;
using NCase.Imp.Vis;

namespace NCase.Autofac
{
    public class NCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<NDsl.Autofac.CoreModule>();
            builder.RegisterModule<NDsl.Autofac.RecPlayModule>();
            builder.RegisterType<CaseGeneratorDirector>().As<Api.Vis.ICaseGeneratorDirector>().InstancePerDependency();
            builder.RegisterType<DumpVisitors>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<CaseGeneratorVisitors>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<CaseSetNode>().As<ICaseSetNode>();
            builder.RegisterType<CaseBuilder>().As<Api.ICaseBuilder>();
        }
    }
}
