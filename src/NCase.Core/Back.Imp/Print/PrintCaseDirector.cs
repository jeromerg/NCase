﻿using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Imp.Print
{
    public class PrintCaseDirector : ActionPayloadDirector<INode, IPrintCaseDirector, IPrintCasePayload>, IPrintCaseDirector
    {
        [NotNull] private readonly IPrintCasePayloadFactory mPrintCasePayloadFactory;

        public PrintCaseDirector([NotNull] IActionPayloadVisitMapper<INode, IPrintCaseDirector, IPrintCasePayload> visitMapper,
                                 [NotNull] IPrintCasePayloadFactory printCasePayloadFactory)
            : base(visitMapper)
        {
            mPrintCasePayloadFactory = printCasePayloadFactory;
        }

        [NotNull]
        public IPrintCasePayload NewPayload()
        {
            return mPrintCasePayloadFactory.Create();
        }
    }
}