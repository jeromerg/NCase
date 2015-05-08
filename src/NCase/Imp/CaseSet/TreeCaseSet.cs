﻿using System;
using NCase.Api.CaseSet;
using NDsl.Api.Core;
using NDsl.Api.Core.Ex;
using NDsl.Imp.Core.Reusable;
using NDsl.Imp.Core.Token;
using NVisitor.Common.Quality;

namespace NCase.Api
{
    public class TreeCaseSet : ITree
    {
        [NotNull] private readonly ITokenWriter mTokenWriter;
        [NotNull] private readonly string mCaseSetName;

        private bool mIsDefined;

        #region Ctor and Factory
        /// <exception cref="ArgumentNullException">The value of 'tokenWriter'/'caseSetName' cannot be null. </exception>
        public TreeCaseSet([NotNull] ITokenWriter tokenWriter, [NotNull] string caseSetName) 
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (caseSetName == null) throw new ArgumentNullException("caseSetName");

            mTokenWriter = tokenWriter;
            mCaseSetName = caseSetName;
        }

        public class Factory : ICaseSetFactory<ITree>
        {
            public ITree Create(ITokenWriter tokenWriter, string name)
            {
                return new TreeCaseSet(tokenWriter, name);
            }
        }
        #endregion

        /// <exception cref="InvalidSyntaxException">Case set has already been defined</exception>
        public IDisposable Define()
        {
            if (mIsDefined)
                throw new InvalidSyntaxException("Case set {0} has already been defined", mCaseSetName);

            mIsDefined = true;
            return new SemanticalBlockDisposable<TreeCaseSet>(mTokenWriter, this);
        }

        public void Ref()
        {
            mTokenWriter.Append(new RefToken<TreeCaseSet>(this));
        }

    }
}