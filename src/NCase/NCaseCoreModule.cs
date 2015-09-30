using Autofac;
using Castle.DynamicProxy;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Back.Api.Replay;
using NCase.Back.Imp.Parse;
using NCase.Back.Imp.Print;
using NCase.Back.Imp.Replay;
using NCase.Front.Imp;
using NDsl.Front.Api;

namespace NCase
{
    public class NCaseCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(new ProxyGenerator());

            builder.RegisterType<OperationDirector>().As<IOperationDirector>().SingleInstance();

            // case and fact factory
            builder.RegisterType<Case.Factory>().As<ICaseFactory>().SingleInstance();
            builder.RegisterType<Fact.Factory>().As<IFactFactory>().SingleInstance();

            // Parser
            builder.RegisterType<ParserGenerator>().As<IParserGenerator>().SingleInstance();

            builder.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency(); // STATEFUL!
            builder.RegisterType<AddChildDirector>().As<IAddChildDirector>().SingleInstance();
            builder.RegisterType<GenerateDirector>().As<IGenerateDirector>().SingleInstance();

            // Replay Director and default visitor
            builder.RegisterType<ReplayDirector>().As<IReplayDirector>().SingleInstance();
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print
            builder.RegisterType<PrintDetailsDirector>().As<IPrintDetailsDirector>().SingleInstance();

            // GetCases
            builder.RegisterType<GetCasesImpl>().AsImplementedInterfaces().SingleInstance();
        }
    }
}