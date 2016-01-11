using System;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class CombinationSetDefinerImp : DefinerImp, CombinationSetDefiner
    {
        private readonly ITokenWriter mTokenWriter;

        public CombinationSetDefinerImp(ITokenWriter tokenWriter, [NotNull] Action onBegin, [NotNull] Action onEnd)
            : base(onBegin, onEnd)
        {
            mTokenWriter = tokenWriter;
        }

        public void Child()
        {
            // TODO JRG
            // mTokenWriter.Append();
        }
    }
}