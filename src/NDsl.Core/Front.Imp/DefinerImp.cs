using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public class DefinerImp : DisposableWithCallbacks, Definer
    {
        public DefinerImp([NotNull] Action onBegin, [NotNull] Action onEnd)
            : base(onBegin, onEnd)
        {
        }
    }
}