using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.RecPlay
{
    public static class InterfaceRecPlayNodeExtensions
    {
        [NotNull] 
        public static string PrintAssignment([NotNull] this IInterfaceRecPlayNode node)
        {
            if (node == null) throw new ArgumentNullException("node");
            return string.Format("{0}={1}", PrintInvocation(node), node.PropertyValue);
        }

        [NotNull] 
        public static string PrintInvocation([NotNull] this IInterfaceRecPlayNode node)
        {
            if (node == null) throw new ArgumentNullException("node");
            return PrintInvocation(node.ContributorName, node.PropertyCallKey);
        }

        [NotNull] 
        public static string PrintInvocation([NotNull] string contributorName, [NotNull] PropertyCallKey propertyCallKey)
        {
            if (contributorName == null) throw new ArgumentNullException("contributorName");
            if (propertyCallKey == null) throw new ArgumentNullException("propertyCallKey");
            return string.Format("{0}.{1}", contributorName, PrintMember(propertyCallKey));
        }

        [NotNull] 
        public static string PrintMember([NotNull] this PropertyCallKey callKey)
        {
            if (callKey == null) throw new ArgumentNullException("callKey");

            return callKey.IndexParameters.Length == 0
                       ? callKey.PropertyName
                       : string.Format("{0}[{1}]",
                                       callKey.PropertyName,
                                       string.Join(", ", callKey.IndexParameters));
        }
    }
}