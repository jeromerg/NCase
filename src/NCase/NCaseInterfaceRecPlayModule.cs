using Autofac;
using NCase.Imp.InterfaceRecPlay;

namespace NCase.Autofac
{
    /// <summary> Requires NCaseCoreModule </summary>
    public class NCaseInterfaceRecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Parser
            builder.RegisterType<ParseVisitors>().AsImplementedInterfaces().SingleInstance();

            // Branching
            builder.RegisterType<GetBranchingKeyVisitors>().AsImplementedInterfaces().SingleInstance();

            // Replay
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // Case Generator
            builder.RegisterType<GenerateCaseVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
