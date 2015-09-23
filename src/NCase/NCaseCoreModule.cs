using Autofac;
using Castle.DynamicProxy;
using NCase.Back.Api.Core.Parse;
using NCase.Back.Api.Core.Print;
using NCase.Back.Api.Core.Replay;
using NCase.Back.Imp.Core.Parse;
using NCase.Back.Imp.Core.Print;
using NCase.Back.Imp.Core.Replay;
using NCase.Front.Imp;

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
            builder.RegisterType<Set.Factory>().As<ISetFactory>().InstancePerDependency();
            builder.RegisterType<Case.Factory>().As<ICaseFactory>().InstancePerDependency();
            builder.RegisterType<Fact.Factory>().As<IFactFactory>().InstancePerDependency();

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