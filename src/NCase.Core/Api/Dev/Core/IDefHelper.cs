using System;
using NCase.Api.Pub;

namespace NCase.Api.Dev.Core
{
    public interface IDefHelper
    {
        ISet Cases { get; }
        TResult Get<TResult>(IDefTransform<TResult> transform);
        IDisposable Define();
        void Begin();
        void End();
        void Ref();
    }
}