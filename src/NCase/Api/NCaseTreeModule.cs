using System.Diagnostics.CodeAnalysis;
using Autofac;
using NCaseFramework.Back.Imp.Tree;
using NCaseFramework.Front.Imp;

namespace NCaseFramework.Front.Api
{
    /// <summary> Requires NCaseCoreModule </summary>
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class NCaseTreeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Case sets
            builder.RegisterType<TreeImp.Factory>().AsImplementedInterfaces().SingleInstance();

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