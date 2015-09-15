using Autofac;
using Castle.DynamicProxy;
using NCase.Api.Dev.Core.GenerateCase;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Core.Replay;
using NCase.Imp.Core.GenerateCase;
using NCase.Imp.Core.Parse;
using NCase.Imp.Core.Replay;
using NDsl;

namespace NCase
{
    public class NCaseCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(new ProxyGenerator());

            // NDsl
            builder.RegisterModule(new NDslCoreModule(new[] { System.Reflection.Assembly.GetExecutingAssembly() }));            
            builder.RegisterModule<NDslRecPlayModule>();

            // Parser
            builder.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency();
            builder.RegisterType<AddChildDirector>().As<IAddChildDirector>().InstancePerDependency();

            // Generate Case Director
            builder.RegisterType<GenerateCaseDirector>().As<IGenerateCaseDirector>().InstancePerDependency();

            // Replay Director and default visitor
            builder.RegisterType<ReplayDirector>().As<IReplayDirector>().InstancePerDependency();
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
