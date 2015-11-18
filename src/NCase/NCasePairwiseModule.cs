using System.Diagnostics.CodeAnalysis;
using Autofac;
using NCaseFramework.Back.Imp.Pairwise;
using NCaseFramework.Front.Imp;
using NUtil.Math.Combinatorics.Pairwise;

namespace NCaseFramework.Front.Ui
{
    /// <summary> Requires NCaseCoreModule </summary>
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class NCasePairwiseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // UI Factory
            builder.RegisterType<PairwiseCombinationsImp.Factory>().AsImplementedInterfaces().SingleInstance();

            // Parser
            builder.RegisterType<ParseVisitors>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<AddChildVisitors>().AsImplementedInterfaces().SingleInstance();

            // Case Generator
            builder.RegisterType<GenerateCaseVisitors>().AsImplementedInterfaces().SingleInstance();

            // PrintDefinition
            builder.RegisterType<PrintDefinitionVisitors>().AsImplementedInterfaces().SingleInstance();

            // pairwise algorithm
            builder.RegisterType<PairwiseGenerator>().As<IPairwiseGenerator>().SingleInstance();
        }
    }
}