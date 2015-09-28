using System;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NCase.Front.Api;
using NDsl.Api.Block;
using NDsl.Api.Core;
using NDsl.Api.Ref;

namespace NCase.Front.Imp
{
    public class DefHelper<TDefId, TDef> : IDefHelper<TDef>
        where TDefId : IDefId
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly TDefId mDefId;
        [NotNull] private readonly ITokenReaderWriter mTokenReaderWriter;
        [NotNull] private readonly string mDefName;

        public DefHelper([NotNull] TDefId defId,
                         [NotNull] string defName,
                         [NotNull] ITokenReaderWriter tokenReaderWriter,
                         [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (defId == null) throw new ArgumentNullException("defId");
            if (defName == null) throw new ArgumentNullException("defName");
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");

            mDefId = defId;
            mTokenReaderWriter = tokenReaderWriter;
            mDefName = defName;
            mCodeLocationUtil = codeLocationUtil;
        }

        public DefSteps State { get; private set; }

        public IDisposable Define()
        {
            return new DisposableWithCallbacks(Begin, End);
        }

        public void Begin()
        {
            if (State > DefSteps.NotDefined)
            {
                throw new InvalidSyntaxException(mCodeLocationUtil.GetCurrentUserCodeLocation(),
                                                 "Case set {0} has already been defined",
                                                 mDefName);
            }

            State = DefSteps.Defining;
            mTokenReaderWriter.Append(new BeginToken<TDefId>(mDefId, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public void End()
        {
            mTokenReaderWriter.Append(new EndToken<TDefId>(mDefId, mCodeLocationUtil.GetCurrentUserCodeLocation()));
            State = DefSteps.Defined;
        }

        public void Ref()
        {
            mTokenReaderWriter.Append(new RefToken<TDefId>(mDefId, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public TResult Perform<TOp, TResult>(TOp operation) where TOp : IOp<TDef, TResult>
        {
            throw new NotImplementedException("TODO JRG");
        }
    }
}