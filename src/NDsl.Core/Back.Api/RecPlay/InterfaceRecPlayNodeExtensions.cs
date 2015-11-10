namespace NDsl.Back.Api.RecPlay
{
    public static class InterfaceRecPlayNodeExtensions
    {
        public static string PrintAssignment(this IInterfaceRecPlayNode node)
        {
            return string.Format("{0}={1}", PrintInvocation(node), node.PropertyValue);
        }

        public static string PrintInvocation(this IInterfaceRecPlayNode node)
        {
            return string.Format("{0}.{1}", node.ContributorName, PrintMember(node));
        }

        public static string PrintMember(this IInterfaceRecPlayNode node)
        {
            return PrintMember(node.PropertyCallKey);
        }

        public static string PrintMember(this PropertyCallKey callKey)
        {
            return callKey.IndexParameters.Length == 0
                       ? callKey.PropertyName
                       : string.Format("{0}[{1}]",
                                       callKey.PropertyName,
                                       string.Join(", ", callKey.IndexParameters));
        }
    }
}