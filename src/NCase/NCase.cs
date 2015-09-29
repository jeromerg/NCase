using System;
using Autofac;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NCase.Front.Api;

namespace NCase
{
    public static class NCase
    {
        #region inner types

        [UsedImplicitly]
        private class Resolver : IResolver
        {
            [NotNull] private readonly IComponentContext mComponentContext;

            public Resolver([NotNull] IComponentContext componentContext)
            {
                if (componentContext == null) throw new ArgumentNullException("componentContext");
                mComponentContext = componentContext;
            }

            public T Resolve<T>()
            {
                return mComponentContext.Resolve<T>();
            }
        }

        #endregion

        public static IBuilder CreateBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<NCaseModule>();
            containerBuilder.RegisterType<Resolver>().As<IResolver>();
            IContainer container = containerBuilder.Build();
            return container.Resolve<IBuilder>();
        }
    }
}