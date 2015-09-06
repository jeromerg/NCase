using System;
using NDsl.Api.Core.Ex;

namespace NCase.Api.Dev
{
    public interface ICaseSet
    {
        /// <exception cref="InvalidSyntaxException">Case set has already been defined</exception>
        IDisposable Define();
        void Ref();
    }
}