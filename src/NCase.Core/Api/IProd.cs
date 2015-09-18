using System;
using NCase.Api.Dev.Core;

namespace NCase.Api
{
    public interface IProd : ICaseSet
    {
        IDisposable Define();
        void Ref();
    }
}