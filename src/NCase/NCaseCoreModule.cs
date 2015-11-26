using System.Diagnostics.CodeAnalysis;
using Autofac;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.Replay;
using NCaseFramework.Back.Imp.Parse;
using NCaseFramework.Back.Imp.Print;
using NCaseFramework.Back.Imp.Replay;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Api.Fact;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Imp;
using NDsl.Back.Api.Record;
using NDsl.Back.Imp.Common;
using NDsl.Front.Api;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class NCaseCoreModule : Module
    {
        protected override void Load(ContainerBuilder cb)
        {
            base.Load(cb);

            cb.RegisterType<BuilderFactory>().As<IBuilderFactory>().SingleInstance();

            cb.RegisterType<TokenStreamFactory>().As<ITokenStreamFactory>().SingleInstance();
            cb.RegisterType<CreateContributor>().As<ICreateContributor>().SingleInstance();

            // CaseEnumerable, Case, Fact factories
            cb.RegisterType<CaseImp.Factory>().As<ICaseFactory>().SingleInstance();
            cb.RegisterType<FactImp.Factory>().As<IFactFactory>().SingleInstance();
            cb.RegisterType<GetCasesSvc>().As<IGetCasesSvc>().SingleInstance();
            cb.RegisterType<ReplayFactSvc>().As<IReplayFactSvc>().SingleInstance();

            // Parser
            cb.RegisterType<ParserGenerator>().As<IParserGenerator>().SingleInstance();

            cb.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency(); // STATEFUL!
            cb.RegisterType<AddChildDirector>().As<IAddChildDirector>().SingleInstance();
            cb.RegisterType<GenerateCasesDirector>().As<IGenerateCasesDirector>().SingleInstance();
            cb.RegisterType<GenerateCasesVisitors>().AsImplementedInterfaces().SingleInstance();

            // SetReplay Director and default visitor
            cb.RegisterType<ReplayFactSvc>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<ReplayDirector>().As<IReplayDirector>().SingleInstance();
            cb.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print Definition
            cb.RegisterType<PrintDefSvc>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintDefinitionPayload.Factory>().As<IPrintDefinitionPayloadFactory>().InstancePerDependency();
                // stateful !!
            cb.RegisterType<PrintDefinitionDirector>().As<IPrintDefinitionDirector>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintDefinitionVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print Case Table
            cb.RegisterType<PrintCaseTableSvc>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintCaseTableDirector>().As<IPrintCaseTableDirector>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintCaseTableVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print Case
            cb.RegisterType<PrintCaseSvc>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintCasePayload.Factory>().As<IPrintCasePayloadFactory>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintCaseDirector>().As<IPrintCaseDirector>().InstancePerDependency(); // stateful !!

            // GetCases
            cb.RegisterType<GetCasesSvc>().AsImplementedInterfaces().SingleInstance();
        }
    }
}