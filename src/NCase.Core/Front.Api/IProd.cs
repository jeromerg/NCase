using System;

namespace NCase.Api.Pub
{
    public interface IProd : IDef
    {
        IDisposable Define();
        void Ref();
    }
}