using System;
using System.Collections.Generic;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NDsl.Api.RecPlay;
using NDsl.Util.Castle;
using NVisitor.Common.Quality;

namespace NDsl.Imp.RecPlay
{
    public class RecPlayInterfacePropertyNode : IRecPlayInterfacePropertyNode
    {
        [NotNull] private readonly IRecPlayInterfaceInterceptor mParentInterceptor;
        
        [NotNull] private readonly string mContributorName;
        [NotNull] private readonly PropertyCallKey mPropertyCallKey;
        [CanBeNull] private readonly object mPropertyValue;
        [NotNull] private readonly ICodeLocation mCodeLocation;

        private bool mIsReplay;

        public RecPlayInterfacePropertyNode([NotNull] IRecPlayInterfaceInterceptor parentInterceptor, [NotNull] string contributorName, [NotNull] PropertyCallKey propertyCallKey, [CanBeNull] object propertyValue, [NotNull] ICodeLocation codeLocation)
        {
            if (parentInterceptor == null) throw new ArgumentNullException("parentInterceptor");
            if (contributorName == null) throw new ArgumentNullException("contributorName");
            if (propertyCallKey == null) throw new ArgumentNullException("propertyCallKey");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mPropertyValue = propertyValue;
            mContributorName = contributorName;
            mParentInterceptor = parentInterceptor;
            mCodeLocation = codeLocation;
            mPropertyCallKey = propertyCallKey;
        }

        public string ContributorName
        {
            get { return mContributorName; }
        }

        public PropertyCallKey PropertyCallKey
        {
            get { return mPropertyCallKey; }
        }

        public object PropertyValue
        {
            get { return mPropertyValue; }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public IEnumerable<INode> Children
        {
            get { yield break; }
        }

        public bool IsReplay
        {
            get { return mIsReplay; }
            set
            {
                if (mIsReplay == value)
                    return;

                mIsReplay = value;

                if (value)
                {
                    mParentInterceptor.SetMode(RecPlay.Mode.Playing);
                    mParentInterceptor.AddReplayPropertyValue(mPropertyCallKey, mPropertyValue);
                }
                else
                {
                    mParentInterceptor.SetMode(RecPlay.Mode.Recording);
                    mParentInterceptor.RemoveReplayPropertyValue(mPropertyCallKey);
                }
            }
        }
    }
}