using System;
using NCase.Api.Dev.Core;

namespace NCase.Api
{
    public interface IPairwise : ICaseSet
    {
        IDisposable Define();
        void Ref();
    }
}