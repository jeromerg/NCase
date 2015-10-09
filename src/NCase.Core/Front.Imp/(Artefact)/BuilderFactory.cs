using System;
using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class BuilderFactory : IBuilderFactory
    {
        private readonly IBookFactory mBookFactory;
        private readonly IServices<IBuilderModel> mServices;

        public BuilderFactory([NotNull] IBookFactory bookFactory, [NotNull] IServices<IBuilderModel> services)
        {
            if (bookFactory == null) throw new ArgumentNullException("bookFactory");
            if (services == null) throw new ArgumentNullException("services");
            mBookFactory = bookFactory;
            mServices = services;
        }

        public IBuilder Create()
        {
            return new Builder(mBookFactory.Create(), mServices);
        }
    }
}