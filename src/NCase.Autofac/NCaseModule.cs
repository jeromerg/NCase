using Autofac;

namespace NCase.Autofac
{
    public class NCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<NDsl.Autofac.CoreModule>();
            builder.RegisterModule<NDsl.Autofac.RecPlayModule>();
            builder.RegisterType<Impl.Vis.ProduceCaseDir>().As<Api.Vis.IProduceCaseDir>().InstancePerDependency();
            builder.RegisterType<Impl.Vis.DumpVisitors>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<Impl.Vis.CaseProducerVisitors>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<Impl.CaseSetNode>().As<Api.ICaseSetNode>();
            builder.RegisterType<Impl.CaseBuilder>().As<Api.ICaseBuilder>();
        }
    }
}
