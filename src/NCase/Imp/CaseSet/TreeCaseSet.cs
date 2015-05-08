using System;
using NCase.Api.CaseSet;
using NDsl.Api.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Util;
using NDsl.Imp.Core.Reusable;
using NDsl.Imp.Core.Token;
using NVisitor.Common.Quality;

namespace NCase.Api
{
    public class TreeCaseSet : ITree
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly ITokenWriter mTokenWriter;
        [NotNull] private readonly string mCaseSetName;

        private bool mIsDefined;

        #region Ctor and Factory
        
        /// <exception cref="ArgumentNullException">The value of 'tokenWriter'/'caseSetName' cannot be null. </exception>
        public TreeCaseSet(
            [NotNull] ITokenWriter tokenWriter, 
            [NotNull] string caseSetName,
            [NotNull] ICodeLocationUtil codeLocationUtil) 
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (caseSetName == null) throw new ArgumentNullException("caseSetName");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");

            mTokenWriter = tokenWriter;
            mCaseSetName = caseSetName;
            mCodeLocationUtil = codeLocationUtil;
        }

        public class Factory : ICaseSetFactory<ITree>
        {

            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mCodeLocationUtil = codeLocationUtil;
            }

            public ITree Create([NotNull] ITokenWriter tokenWriter, [NotNull] string name)
            {
                if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
                if (name == null) throw new ArgumentNullException("name");
                return new TreeCaseSet(tokenWriter, name, mCodeLocationUtil);
            }
        }
        #endregion

        /// <exception cref="InvalidSyntaxException">Case set has already been defined</exception>
        public IDisposable Define()
        {
            if (mIsDefined)
                throw new InvalidSyntaxException("Case set {0} has already been defined", mCaseSetName);

            mIsDefined = true;

            return new SemanticalBlockDisposable<TreeCaseSet>(mCodeLocationUtil, mTokenWriter, this);
        }

        public void Ref()
        {
            mTokenWriter.Append(new RefToken<TreeCaseSet>(this, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

    }
}