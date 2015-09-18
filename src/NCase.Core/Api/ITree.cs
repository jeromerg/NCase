using System;
using NCase.Api.Dev.Core;

namespace NCase.Api
{
    public interface ITree : ICaseSet
    {
        IDisposable Define();
        void Ref();
    }
}