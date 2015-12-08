using System;
using Autofac;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api
{
    public class ServiceSet<TTarget> : IServiceSet<TTarget>
    {
        private readonly IComponentContext mComponentContext;

        public ServiceSet([NotNull] IComponentContext componentContext)
        {
            if (componentContext == null) throw new ArgumentNullException("componentContext");
            mComponentContext = componentContext;
        }

        [NotNull]
        public TService GetService<TService>() where TService : IService<TTarget>
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return mComponentContext.Resolve<TService>();
        }
    }
}