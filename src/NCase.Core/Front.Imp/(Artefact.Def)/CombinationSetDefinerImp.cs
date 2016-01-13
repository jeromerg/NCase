using System;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class CombinationSetDefinerImp : DefinerImp, CombinationSetDefiner
    {
        [NotNull] private readonly ICodeLocationFactory mCodeLocationFactory;
        [NotNull] private readonly ITokenWriter mTokenWriter;

        public CombinationSetDefinerImp([NotNull] ITokenWriter tokenWriter,
                                        [NotNull] ICodeLocationFactory codeLocationFactory,
                                        [NotNull] Action onBegin,
                                        [NotNull] Action onEnd)
            : base(onBegin, onEnd)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (codeLocationFactory == null) throw new ArgumentNullException("codeLocationFactory");
            mTokenWriter = tokenWriter;
            mCodeLocationFactory = codeLocationFactory;
        }

        public void Branch()
        {
            CodeLocation codeLocation = mCodeLocationFactory.GetCurrentUserCodeLocation();
            mTokenWriter.Append(new NullToken(codeLocation));
        }
    }
}