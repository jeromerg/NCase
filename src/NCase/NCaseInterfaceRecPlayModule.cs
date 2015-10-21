using Autofac;
using NCaseFramework.Back.Imp.InterfaceRecPlay;

namespace NCaseFramework.Front.Ui
{
    /// <summary> Requires NCaseCoreModule </summary>
    public class NCaseInterfaceRecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Parser
            builder.RegisterType<ParseVisitors>().AsImplementedInterfaces().SingleInstance();
            // remark: AddChild visitors are located in Parse and Tree modules

            // Replay
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // Case Generator
            builder.RegisterType<GenerateCaseVisitors>().AsImplementedInterfaces().SingleInstance();

            // PrintLine
            builder.RegisterType<PrintDefinitionVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}