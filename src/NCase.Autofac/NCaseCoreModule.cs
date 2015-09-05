using Autofac;
using NDsl.Api.Core;
using NDsl.Autofac;
using NDsl.Imp.Core;
using NCase.Api;
using NCase.Api.Dev;
using NCase.Imp.Core;

namespace NCase.Autofac
{
    public class NCaseCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            // NDsl
            builder.RegisterModule<NDslCoreModule>();            
            builder.RegisterModule<NDslRecPlayModule>();

            // Parser
            builder.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency();

            // BranchingKeyDirector and default visitor
            builder.RegisterType<GetBranchingKeyDirector>().As<IGetBranchingKeyDirector>().InstancePerDependency();
            builder.RegisterType<GetBranchingKeyVisitors>().AsImplementedInterfaces().SingleInstance();

            // Generate Case Director
            builder.RegisterType<GenerateCaseDirector>().As<IGenerateCaseDirector>().InstancePerDependency();

            // Replay Director and default visitor
            builder.RegisterType<ReplayDirector>().As<IReplayDirector>().InstancePerDependency();
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
