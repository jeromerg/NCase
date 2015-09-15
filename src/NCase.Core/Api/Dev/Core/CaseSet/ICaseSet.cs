using System;
using NDsl.Api.Dev.Core.Ex;

namespace NCase.Api.Dev.Core.CaseSet
{
    public interface ICaseSet
    {
        /// <exception cref="InvalidSyntaxException">Case set has already been defined</exception>
        IDisposable Define();
        void Ref();
    }
}