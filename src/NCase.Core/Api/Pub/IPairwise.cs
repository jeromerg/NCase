using System;

namespace NCase.Api.Pub
{
    public interface IPairwise : IDef
    {
        IDisposable Define();
        void Ref();
    }
}