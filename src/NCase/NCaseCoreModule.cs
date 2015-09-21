using Autofac;
using Castle.DynamicProxy;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Core.Print;
using NCase.Api.Dev.Core.Replay;
using NCase.Imp.Core;
using NCase.Imp.Core.Parse;
using NCase.Imp.Core.Print;
using NCase.Imp.Core.Replay;

namespace NCase
{
    public class NCaseCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(new ProxyGenerator());

            // Def helper
            builder.RegisterType<DefHelperFactory>().As<IDefHelperFactory>().InstancePerDependency();

            // case and fact factory
            builder.RegisterType<SetFactory>().As<ISetFactory>().InstancePerDependency();
            builder.RegisterType<CaseFactory>().As<ICaseFactory>().InstancePerDependency();
            builder.RegisterType<FactFactory>().As<IFactFactory>().InstancePerDependency();

            // Parser
            builder.RegisterType<ParserGenerator>().As<IParserGenerator>().InstancePerDependency();

            builder.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency();
            builder.RegisterType<AddChildDirector>().As<IAddChildDirector>().InstancePerDependency();
            builder.RegisterType<GenerateDirector>().As<IGenerateDirector>().InstancePerDependency();

            // Replay Director and default visitor
            builder.RegisterType<ReplayDirector>().As<IReplayDirector>().InstancePerDependency();
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print
            builder.RegisterType<PrintDetailsDirector>().As<IPrintDetailsDirector>().InstancePerDependency();

        }
    }
}