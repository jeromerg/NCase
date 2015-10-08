using Autofac;
using NCase.Back.Imp.Seq;
using NCase.Front.Imp;
using NCase.Front.Imp.Op;

namespace NCase.Front.Ui
{
    /// <summary> Requires NCaseCoreModule </summary>
    public class NCaseSeqModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Case sets
            builder.RegisterType<Seq.Factory>().As<ISeqFactory>().SingleInstance();

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