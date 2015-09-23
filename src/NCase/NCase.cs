using System;
using Autofac;
using NCase.Back.Api.Core;
using NCase.Front.Api;
using NVisitor.Common.Quality;

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
            var builder = new ContainerBuilder();
            builder.RegisterModule<NCaseModule>();
            builder.RegisterType<Resolver>().As<IResolver>();
            IContainer container = builder.Build();
            return container.Resolve<IBuilder>();
        }
    }
}