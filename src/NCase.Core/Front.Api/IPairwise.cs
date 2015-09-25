using System;

namespace NCase.Front.Api
{
    public interface IPairwise : IDef<IPairwise>
    {
        IDisposable Define();
        void Ref();
    }
}