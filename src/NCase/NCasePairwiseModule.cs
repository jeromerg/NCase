using Autofac;
using NCase.Back.Imp.Pairwise;
using NCase.Front.Imp;
using NCase.Front.Imp.Op;

namespace NCase.Front.Ui
{
    /// <summary> Requires NCaseCoreModule </summary>
    public class NCasePairwiseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // UI Factory
            builder.RegisterType<Pairwise.Factory>().As<IPairwiseFactory>().SingleInstance();

            // Parser
            builder.RegisterType<ParseVisitors>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<AddChildVisitors>().AsImplementedInterfaces().SingleInstance();

            // Case Generator
            builder.RegisterType<GenerateCaseVisitors>().AsImplementedInterfaces().SingleInstance();

            // PrintDefinition
            builder.RegisterType<PrintDefinitionVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}