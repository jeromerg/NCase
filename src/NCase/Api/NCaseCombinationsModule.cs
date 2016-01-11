﻿using System.Diagnostics.CodeAnalysis;
using Autofac;
using NCaseFramework.Back.Imp.CombinationSet;
using NCaseFramework.Front.Imp;

namespace NCaseFramework.Front.Api
{
    /// <summary> Requires NCaseCoreModule </summary>
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class NCaseCombinationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Case sets
            builder.RegisterType<CombinationSetImp.Factory>().AsImplementedInterfaces().SingleInstance();

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