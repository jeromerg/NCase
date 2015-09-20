using System;
using NCase.Api.Dev.Core;

namespace NCase.Api
{
    public interface IProd : IDef
    {
        IDisposable Define();
        void Ref();
    }
}