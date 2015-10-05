using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Block;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class DefImpBase<TDef, TDefId, TDefImp>
        : ArtefactImpBase<TDef, TDefImp>, IDef<TDef>, IDefImp
        where TDef : IDef<TDef>
        where TDefId : IDefId
        where TDefImp : IDefImp<TDefId>
    {
        [NotNull] private readonly ITokenReaderWriter mTokenReaderWriter;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        protected DefImpBase([NotNull] ITokenReaderWriter tokenReaderWriter,
                             [NotNull] ICodeLocationUtil codeLocationUtil,
                             [NotNull] IOperationDirector operationDirector)
            : base(operationDirector)
        {
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mTokenReaderWriter = tokenReaderWriter;
            mCodeLocationUtil = codeLocationUtil;
        }


        public DefSteps State { get; private set; }

        public IDisposable Define()
        {
            return new DisposableWithCallbacks(Begin, End);
        }

        public void Ref()
        {
            TokenReaderWriter.Append(new RefToken<TDefId>(ThisDefImpl.Id, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        [NotNull] public ITokenReaderWriter TokenReaderWriter
        {
            get { return mTokenReaderWriter; }
        }

        public IDefId DefId
        {
            get { return ThisDefImpl.Id; }
        }

        public void Begin()
        {
            if (State > DefSteps.NotDefined)
            {
                throw new InvalidSyntaxException(mCodeLocationUtil.GetCurrentUserCodeLocation(),
                                                 "Case set {0} has already been defined",
                                                 DefId.Name);
            }

            State = DefSteps.Defining;
            TokenReaderWriter.Append(new BeginToken<TDefId>(ThisDefImpl.Id, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public void End()
        {
            TokenReaderWriter.Append(new EndToken<TDefId>(ThisDefImpl.Id, mCodeLocationUtil.GetCurrentUserCodeLocation()));
            State = DefSteps.Defined;
        }
    }
}