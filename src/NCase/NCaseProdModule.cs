using Autofac;
using NCase.Back.Imp.Prod;
using NCase.Front.Api.Prod;
using NCase.Front.Imp.Prod;

namespace NCase.Front.Ui
{
    /// <summary> Requires NCaseCoreModule, NCaseSeqModule </summary>
    public class NCaseProdModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Case sets
            builder.RegisterType<ProdFactory>().As<IProdFactory>().SingleInstance();

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