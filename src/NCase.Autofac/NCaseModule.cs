using Autofac;
using NCase.Api;
using NCase.Api.Vis;
using NCase.Imp;
using NCase.Imp.Vis;
using NDsl.Autofac;
using NDsl.Imp.Core;

namespace NCase.Autofac
{
    public class NCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // modules
            builder.RegisterModule<CoreModule>();            
            builder.RegisterModule<RecPlayModule>();
            
            // Case sets
            builder.RegisterType<TreeCaseSet.Factory>().AsImplementedInterfaces().InstancePerDependency();

            // Parser
            builder.RegisterType<ParserDirector>().As<IParserDirector>().InstancePerDependency();
            builder.RegisterType<ParserVisitors>().AsImplementedInterfaces().SingleInstance();

            // Case Generator
            builder.RegisterType<CaseGeneratorDirector>().As<ICaseGeneratorDirector>().InstancePerDependency();
            builder.RegisterType<CaseGeneratorVisitors>().AsImplementedInterfaces().SingleInstance();

            // Replay
            builder.RegisterType<ReplayDirector>().As<IReplayDirector>().InstancePerDependency();
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // case builder 
            builder.RegisterType<TokenStream>().As<ITokenReaderWriter>();
            builder.RegisterType<CaseBuilder>().As<ICaseBuilder>().InstancePerDependency();
        }
    }
}
