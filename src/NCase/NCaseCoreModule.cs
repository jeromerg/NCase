using System.Collections.Generic;
using Autofac;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Back.Api.Replay;
using NCase.Back.Imp.Parse;
using NCase.Back.Imp.Print;
using NCase.Back.Imp.Replay;
using NCase.Front.Imp.Artefact;
using NCase.Front.Imp.Op;
using NDsl.Front.Api;

namespace NCase
{
    public class NCaseCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // OPERATION DIRECTOR
            builder.RegisterType<OperationDirector>()
                   .As<IOperationDirector>().SingleInstance()
                   .OnActivated(
                                eventArgs =>
                                eventArgs.Instance.InitializeDirector(
                                                                      eventArgs.Context
                                                                               .Resolve<IEnumerable<IOperationVisitorClass>>()));

            // CaseEnumerable, Case, Fact factories
            builder.RegisterType<CaseEnumerableImp.Factory>().AsSelf().SingleInstance();
            builder.RegisterType<CaseImp.Factory>().AsSelf().SingleInstance();
            builder.RegisterType<FactImp.Factory>().AsSelf().SingleInstance();

            // Parser
            builder.RegisterType<ParserGenerator>().As<IParserGenerator>().SingleInstance();

            builder.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency(); // STATEFUL!
            builder.RegisterType<AddChildDirector>().As<IAddChildDirector>().SingleInstance();
            builder.RegisterType<GenerateDirector>().As<IGenerateDirector>().SingleInstance();

            // Replay Director and default visitor
            builder.RegisterType<ReplayImp>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ReplayDirector>().As<IReplayDirector>().SingleInstance();
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print Definition
            builder.RegisterType<PrintDefinitionImpl>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PrintDefinitionDirector>().As<IPrintDefinitionDirector>().InstancePerDependency(); // stateful !!
            builder.RegisterType<PrintDefinitionVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print Case Table
            builder.RegisterType<PrintCaseTableImpl>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PrintCaseTableDirector>().As<IPrintCaseTableDirector>().InstancePerDependency(); // stateful !!
            builder.RegisterType<PrintCaseTableVisitors>().AsImplementedInterfaces().SingleInstance();

            // GetCases
            builder.RegisterType<GetCasesImpl>().AsImplementedInterfaces().SingleInstance();
        }
    }
}