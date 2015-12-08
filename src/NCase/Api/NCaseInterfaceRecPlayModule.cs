using System.Diagnostics.CodeAnalysis;
using Autofac;
using NCaseFramework.Back.Imp.InterfaceRecPlay;

namespace NCaseFramework.Front.Api
{
    /// <summary> Requires NCaseCoreModule </summary>
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class NCaseInterfaceRecPlayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Parser
            builder.RegisterType<ParseVisitors>().AsImplementedInterfaces().SingleInstance();
            // remark: AddChild visitors are located in Parse and Tree modules

            // SetReplay
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // Case Generator
            builder.RegisterType<GenerateCaseVisitors>().AsImplementedInterfaces().SingleInstance();

            // PrintLine
            builder.RegisterType<PrintDefinitionVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}