using Autofac;
using NCase.Imp.Prod;

namespace NCase.Autofac
{
    /// <summary> Requires NCaseCoreModule </summary>
    public class NCaseProdModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Case sets
            builder.RegisterType<ProdCaseSetFactory>().AsImplementedInterfaces().SingleInstance();

            // Parser
            builder.RegisterType<ParseVisitors>().AsImplementedInterfaces().SingleInstance();

            // Branching
            builder.RegisterType<GetBranchingKeyVisitors>().AsImplementedInterfaces().SingleInstance();
            
            // Case Generator
            builder.RegisterType<GenerateCaseVisitors>().AsImplementedInterfaces().SingleInstance();

        }
    }
}
