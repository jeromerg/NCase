using System;
using NCase.Api.Dev.Core;

namespace NCase.Api
{
    public interface IPairwise : IDef
    {
        IDisposable Define();
        void Ref();
    }
}