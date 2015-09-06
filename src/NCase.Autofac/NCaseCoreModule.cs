using System.Reflection;
using Autofac;
using Castle.DynamicProxy;
using NDsl.Autofac;
using NCase.Api.Dev;
using NCase.Api.Dev.Core.GenerateCase;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Core.Replay;
using NCase.Imp.Core;
using NCase.Imp.Core.GenerateCase;
using NCase.Imp.Core.Parse;
using NCase.Imp.Core.Replay;
using Module = Autofac.Module;

namespace NCase.Autofac
{
    public class NCaseCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(new ProxyGenerator());

            // NDsl
            builder.RegisterModule(new NDslCoreModule(new[] { Assembly.GetExecutingAssembly() }));            
            builder.RegisterModule<NDslRecPlayModule>();

            // Parser
            builder.RegisterType<ParseDirector>().As<IParseDirector>().InstancePerDependency();

            // BranchingKeyDirector and default visitor
            builder.RegisterType<GetBranchingKeyDirector>().As<IGetBranchingKeyDirector>().InstancePerDependency();
            builder.RegisterType<GetBranchingKeyVisitors>().AsImplementedInterfaces().SingleInstance();

            // Generate Case Director
            builder.RegisterType<GenerateCaseDirector>().As<IGenerateCaseDirector>().InstancePerDependency();

            // Replay Director and default visitor
            builder.RegisterType<ReplayDirector>().As<IReplayDirector>().InstancePerDependency();
            builder.RegisterType<ReplayVisitors>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
