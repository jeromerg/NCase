using System;
using JetBrains.Annotations;
using NDsl.Back.Api;
using NDsl.Back.Api.Block;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class DefImpBase : IDefImp
    {
        [NotNull] private readonly ITokenReaderWriter mTokenReaderWriter;
        [NotNull] private readonly string mDefName;

        protected DefImpBase([NotNull] string defName,
                             [NotNull] ITokenReaderWriter tokenReaderWriter)
        {
            if (defName == null) throw new ArgumentNullException("defName");
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");

            mTokenReaderWriter = tokenReaderWriter;
            mDefName = defName;
        }

        [NotNull] public ITokenReaderWriter TokenReaderWriter
        {
            get { return mTokenReaderWriter; }
        }

        [NotNull] public string DefName
        {
            get { return mDefName; }
        }

        public abstract IDefId DefId { get; }
    }

    public abstract class DefImpBase<TDef, TDefId, TDefImp>
        : DefImpBase, IDef<TDef>
        where TDef : IDef<TDef>
        where TDefId : IDefId
        where TDefImp : IDefImp<TDefId>
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly IOperationDirector mOperationDirector;

        protected DefImpBase([NotNull] string defName,
                             [NotNull] ITokenReaderWriter tokenReaderWriter,
                             [NotNull] ICodeLocationUtil codeLocationUtil,
                             [NotNull] IOperationDirector operationDirector)
            : base(defName, tokenReaderWriter)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (operationDirector == null) throw new ArgumentNullException("operationDirector");
            mCodeLocationUtil = codeLocationUtil;
            mOperationDirector = operationDirector;
        }

        public override IDefId DefId
        {
            get { return ThisDefImpl.Id; }
        }

        protected abstract TDefImp ThisDefImpl { get; }

        public DefSteps State { get; private set; }

        public TResult Perform<TOp, TResult>(TOp operation) where TOp : IOp<TDef, TResult>
        {
            return mOperationDirector.Perform(operation, ThisDefImpl);
        }

        public IDisposable Define()
        {
            return new DisposableWithCallbacks(Begin, End);
        }

        public void Ref()
        {
            TokenReaderWriter.Append(new RefToken<TDefId>(ThisDefImpl.Id, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public void Begin()
        {
            if (State > DefSteps.NotDefined)
            {
                throw new InvalidSyntaxException(mCodeLocationUtil.GetCurrentUserCodeLocation(),
                                                 "Case set {0} has already been defined",
                                                 DefName);
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