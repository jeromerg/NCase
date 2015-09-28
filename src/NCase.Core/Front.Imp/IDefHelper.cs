using System;
using NCase.Front.Api;

namespace NCase.Front.Imp
{
    public interface IDefHelper<TDef>
    {
        IDisposable Define();
        void Begin();
        void End();
        void Ref();
        TResult Perform<TOp, TResult>(TOp operation) where TOp : IOp<TDef, TResult>;
    }
}