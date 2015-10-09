using Autofac;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Back.Api.Replay;
using NCase.Back.Imp.Parse;
using NCase.Back.Imp.Print;
using NCase.Back.Imp.Replay;
using NCase.Front.Api.Builder;
using NCase.Front.Api.Case;
using NCase.Front.Api.CaseEnumerable;
using NCase.Front.Api.Fact;
using NCase.Front.Api.SetDef;
using NCase.Front.Imp.Builder;
using NCase.Front.Imp.Case;
using NCase.Front.Imp.CaseEnumerable;
using NCase.Front.Imp.Fact;
using NCase.Front.Imp.Tool;
using NDsl.Back.Api.Book;
using NDsl.Back.Imp.Common;

namespace NCase.Front.Ui
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
            cb.RegisterType<CaseEnumerablefactory>().As<ICaseEnumerableFactory>().SingleInstance();
            cb.RegisterType<CaseFactory>().As<ICaseFactory>().SingleInstance();
            cb.RegisterType<FactFactory>().As<IFactFactory>().SingleInstance();
            cb.RegisterType<GetCases>().As<IGetCases>().SingleInstance();
            cb.RegisterType<ReplayCases>().As<IReplayCases>().SingleInstance();

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
            cb.RegisterType<PrintTable>().AsImplementedInterfaces().SingleInstance();
            cb.RegisterType<PrintCaseTableDirector>().As<IPrintCaseTableDirector>().InstancePerDependency(); // stateful !!
            cb.RegisterType<PrintCaseTableVisitors>().AsImplementedInterfaces().SingleInstance();

            // GetCases
            cb.RegisterType<GetCases>().AsImplementedInterfaces().SingleInstance();
        }
    }
}