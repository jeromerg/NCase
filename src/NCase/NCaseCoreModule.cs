﻿using Autofac;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.Replay;
using NCaseFramework.Back.Imp.Parse;
using NCaseFramework.Back.Imp.Print;
using NCaseFramework.Back.Imp.Replay;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Api.CaseEnumerable;
using NCaseFramework.Front.Api.Fact;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Imp;
using NDsl.Back.Api.TokenStream;
using NDsl.Back.Imp.Common;
using NDsl.Front.Api;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Ui
{
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
            cb.RegisterType<GetCasesImp>().As<IGetCases>().SingleInstance();
            cb.RegisterType<ReplayFact>().As<IReplayFact>().SingleInstance();

            // Parser
            cb.RegisterType<ParserGenerator>().As<IParserGenerator>().SingleInstance();

            cb.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency(); // STATEFUL!
            cb.RegisterType<AddChildDirector>().As<IAddChildDirector>().SingleInstance();
            cb.RegisterType<GenerateCasesDirector>().As<IGenerateCasesDirector>().SingleInstance();
            cb.RegisterType<GenerateCasesVisitors>().AsImplementedInterfaces().SingleInstance();

            // SetReplay Director and default visitor
            cb.RegisterType<ReplayFact>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<ReplayDirector>().As<IReplayDirector>().SingleInstance();
            cb.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print Definition
            cb.RegisterType<PrintDefImp>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintDefinitionDirector>().As<IPrintDefinitionDirector>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintDefinitionVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print Case Table
            cb.RegisterType<PrintCaseTableImp>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintCaseTableDirector>().As<IPrintCaseTableDirector>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintCaseTableVisitors>().AsImplementedInterfaces().SingleInstance();

            // Print Case
            cb.RegisterType<PrintCaseImp>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintCasePayload.Factory>().As<IPrintCasePayloadFactory>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintCaseDirector>().As<IPrintCaseDirector>().InstancePerDependency(); // stateful !!

            // GetCases
            cb.RegisterType<GetCasesImp>().AsImplementedInterfaces().SingleInstance();
        }
    }
}