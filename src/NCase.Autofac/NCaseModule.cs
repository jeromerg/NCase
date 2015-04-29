using Autofac;
using NCase.Api;
using NCase.Api.Vis;
using NCase.Imp;
using NCase.Imp.Vis;
using NDsl.Autofac;

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
            
            // Parser (visit IToken flat-list and produces INode AST)
            builder.RegisterType<ParserDirector>()        .As<IParserDirector>()       .InstancePerDependency();
            builder.RegisterType<ParserVisitors>()        .AsImplementedInterfaces().SingleInstance();
            
            // Cases generator (lazily visit INode AST and construct all cases one by one)
            builder.RegisterType<CaseGeneratorDirector>() .As<ICaseGeneratorDirector>().InstancePerDependency();
            builder.RegisterType<CaseGeneratorVisitors>() .AsImplementedInterfaces().SingleInstance();

            // Replay visitors (visit the INode in a case-set and prepare for replay, i.e. load responses into the interceptors)
            builder.RegisterType<ReplayDirector>()        .As<IReplayDirector>()       .InstancePerDependency();
            builder.RegisterType<ReplayVisitors>()        .AsImplementedInterfaces().SingleInstance();

            // case builder 
            builder.RegisterType<CaseBuilder>().As<ICaseBuilder>().InstancePerDependency();
        }
    }
}
