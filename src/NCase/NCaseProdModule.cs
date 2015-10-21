using Autofac;
using NCaseFramework.Back.Imp.Prod;
using NCaseFramework.Front.Imp;

namespace NCaseFramework.Front.Ui
{
    /// <summary> Requires NCaseCoreModule, NCaseSeqModule </summary>
    public class NCaseProdModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Case sets
            builder.RegisterType<AllCombinationsImp.Factory>().AsImplementedInterfaces().SingleInstance();

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