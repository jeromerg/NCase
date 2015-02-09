using System;
using NDsl.Api.Core.Util;
using NDsl.Api.RecPlay;
using NDsl.Util.Castle;
using NVisitor.Common.Quality;

namespace NDsl.Impl.RecPlay
{
    public class RecPlayInterfacePropertyNode : IRecPlayInterfacePropertyNode
    {
        [NotNull] private readonly IRecPlayInterfaceInterceptor mParentInterceptor;
        
        [NotNull] private readonly string mContributorName;
        [NotNull] private readonly PropertyCallKey mPropertyCallKey;
        [CanBeNull] private readonly object mValue;
        [NotNull] private readonly ICodeLocation mCodeLocation;

        public RecPlayInterfacePropertyNode([NotNull] IRecPlayInterfaceInterceptor parentInterceptor, [NotNull] string contributorName, [NotNull] PropertyCallKey propertyCallKey, [CanBeNull] object value, [NotNull] ICodeLocation codeLocation)
        {
            if (parentInterceptor == null) throw new ArgumentNullException("parentInterceptor");
            if (contributorName == null) throw new ArgumentNullException("contributorName");
            if (propertyCallKey == null) throw new ArgumentNullException("propertyCallKey");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mValue = value;
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

        public object Value
        {
            get { return mValue; }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public void Replay()
        {
            mParentInterceptor.AddReplayPropertyValue(mPropertyCallKey, mValue);
        }
    }
}