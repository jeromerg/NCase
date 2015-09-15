﻿using Autofac;
using NCase.Api;
using NCase.Imp;

namespace NCase
{
    public class NCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // modules
            builder.RegisterModule<NCaseCoreModule>();
            builder.RegisterModule<NCaseInterfaceRecPlayModule>(); 
            builder.RegisterModule<NCaseTreeModule>(); 
            builder.RegisterModule<NCaseProdModule>(); 
                       
            // case builder 
            builder.RegisterType<CaseBuilder>().As<ICaseBuilder>().InstancePerDependency();
        }
    }
}
