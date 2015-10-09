using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class CaseFactory : ICaseFactory
    {
        [NotNull] private readonly IServices<ICaseModel> mServices;

        public CaseFactory([NotNull] IServices <ICaseModel> services)
        {
            if (services == null) throw new ArgumentNullException("services");
            mServices = services;
        }

        public ICase Create(IEnumerable<INode> factNodes)
        {
            return new Case(factNodes, mServices);
        }
    }
}