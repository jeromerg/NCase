using System;

namespace NCase.Front.Api
{
    public interface IPairwise : IDef
    {
        IDisposable Define();
        void Ref();
    }
}