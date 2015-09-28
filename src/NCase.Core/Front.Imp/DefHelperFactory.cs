using System;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NCase.Back.Api.Parse;
using NDsl.Api.Core;

namespace NCase.Front.Imp
{
    public class DefHelperFactory : IDefHelperFactory
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public DefHelperFactory([NotNull] IParserGenerator parserGenerator,
                                [NotNull] ICodeLocationUtil codeLocationUtil,
                                [NotNull] ICaseFactory caseFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");

            mCodeLocationUtil = codeLocationUtil;
        }

        public DefHelper<TDefId, TDef> CreateDefHelper<TDefId, TDef>(TDefId def,
                                                                     string defName,
                                                                     ITokenReaderWriter tokenReaderWriter)
            where TDefId : IDefId
        {
            return new DefHelper<TDefId, TDef>(def, defName, tokenReaderWriter, mCodeLocationUtil);
        }
    }
}