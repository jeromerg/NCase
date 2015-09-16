﻿using Autofac;
using NCase.Api;
using NCase.Imp;
using NDsl;

namespace NCase
{
    public class NCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // modules
            builder.RegisterModule(new NDslCoreModule(new[] { System.Reflection.Assembly.GetExecutingAssembly() }));
            builder.RegisterModule<NDslRecPlayModule>();
            builder.RegisterModule<NCaseCoreModule>();
            builder.RegisterModule<NCaseInterfaceRecPlayModule>(); 
            builder.RegisterModule<NCaseTreeModule>(); 
            builder.RegisterModule<NCaseProdModule>(); 
                       
            // case builder 
            builder.RegisterType<CaseBuilder>().As<ICaseBuilder>().InstancePerDependency();
        }
    }
}
