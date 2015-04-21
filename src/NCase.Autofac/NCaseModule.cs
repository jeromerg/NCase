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
            
            // directors
            builder.RegisterType<CaseGeneratorDirector>() .As<ICaseGeneratorDirector>().InstancePerDependency();
            builder.RegisterType<ParserDirector>()        .As<IParserDirector>()       .InstancePerDependency();
            builder.RegisterType<ReplayDirector>()        .As<IReplayDirector>()       .InstancePerDependency();

            // visitors
            builder.RegisterType<CaseGeneratorVisitors>() .AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ParserVisitors>()        .AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ReplayVisitors>()        .AsImplementedInterfaces().SingleInstance();

            // case builder 
            builder.RegisterType<CaseBuilder>().As<ICaseBuilder>().InstancePerDependency();
        }
    }
}
