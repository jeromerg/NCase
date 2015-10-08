using System.Collections.Generic;
using Autofac;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Back.Api.Replay;
using NCase.Back.Imp.Parse;
using NCase.Back.Imp.Print;
using NCase.Back.Imp.Replay;
using NCase.Front.Imp;
using NCase.Front.Imp.Op;

namespace NCase.Front.Ui
{
    public class NCaseCoreModule : Module
    {
        protected override void Load(ContainerBuilder cb)
        {
            base.Load(cb);

            cb.RegisterType<Builder>().As<IBuilder>().InstancePerDependency();

            // CaseEnumerable, Case, Fact factories
            cb.RegisterType<CaseEnumerable.Factory>().AsSelf().SingleInstance();
            cb.RegisterType<Case.Factory>().AsSelf().SingleInstance();
            cb.RegisterType<Fact.Factory>().AsSelf().SingleInstance();

            // Parser
            cb.RegisterType<ParserGenerator>().As<IParserGenerator>().SingleInstance();

            cb.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency(); // STATEFUL!
            cb.RegisterType<AddChildDirector>().As<IAddChildDirector>().SingleInstance();
            cb.RegisterType<GenerateCasesDirector>().As<IGenerateCasesDirector>().SingleInstance();
            cb.RegisterType<GenerateCasesVisitors>().AsImplementedInterfaces().SingleInstance();

            // Replay Director and default visitor
            cb.RegisterType<ReplayCases>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<ReplayDirector>().As<IReplayDirector>().SingleInstance();
            cb.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // PrintLine Definition
            cb.RegisterType<PrintDef>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintDefinitionDirector>().As<IPrintDefinitionDirector>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintDefinitionVisitors>().AsImplementedInterfaces().SingleInstance();

            // PrintLine Case Table
            cb.RegisterType<Imp.Op.PrintTable>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintCaseTableDirector>().As<IPrintCaseTableDirector>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintCaseTableVisitors>().AsImplementedInterfaces().SingleInstance();

            // GetCases
            cb.RegisterType<GetCases>().AsImplementedInterfaces().SingleInstance();
        }
    }
}