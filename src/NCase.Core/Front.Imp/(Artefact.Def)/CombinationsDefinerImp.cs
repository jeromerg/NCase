using System;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class CombinationsDefinerImp : DefinerImp, CombinationsDefiner
    {
        private readonly ITokenWriter mTokenWriter;

        public CombinationsDefinerImp(ITokenWriter tokenWriter, [NotNull] Action onBegin, [NotNull] Action onEnd)
            : base(onBegin, onEnd)
        {
            mTokenWriter = tokenWriter;
        }

        public void Fork()
        {
            // TODO JRG
            // mTokenWriter.Append();
        }

        public void Child()
        {
            // TODO JRG
            // mTokenWriter.Append();
        }
    }
}