using Autofac;
using NCase.Api;
using NCase.Api.Vis;
using NCase.Imp;
using NCase.Imp.Vis;
using NCase.Imp.Vis.CaseSets;
using NCase.Imp.Vis.TokenStream;
using NCase.Imp.Vis.TreeCaseSet;
using NDsl.Api.Core;
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
            builder.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency();
            builder.RegisterType<ParseVisitors>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<TreeCaseSetInsertChildDirector>().As<ITreeCaseSetInsertChildDirector>().InstancePerDependency();
            builder.RegisterType<TreeCaseSetInsertChildVisitors>().AsImplementedInterfaces().SingleInstance();

            // Case Generator
            builder.RegisterType<GenerateCaseDirector>().As<IGenerateCaseDirector>().InstancePerDependency();
            builder.RegisterType<GenerateCaseVisitors>().AsImplementedInterfaces().SingleInstance();

            // Replay
            builder.RegisterType<ReplayDirector>().As<IReplayDirector>().InstancePerDependency();
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();

            // case builder 
            builder.RegisterType<TokenStream>().As<ITokenReaderWriter>();
            builder.RegisterType<CaseBuilder>().As<ICaseBuilder>().InstancePerDependency();
        }
    }
}
