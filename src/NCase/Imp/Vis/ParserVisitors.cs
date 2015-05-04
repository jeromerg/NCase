using System;
using System.Linq;
using Castle.DynamicProxy;
using NCase.Api;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NCase.Imp.Nod;
using NDsl.Api.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Util;
using NDsl.Api.RecPlay;
using NDsl.Imp.Core.Token;
using NDsl.Imp.RecPlay;
using NDsl.Util.Castle;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis
{
    public class ParserVisitors
        : IVisitor<IToken, IParserDirector, BeginToken<TreeCaseSet>>
        , IVisitor<IToken, IParserDirector, EndToken<TreeCaseSet>>
        , IVisitor<IToken, IParserDirector, InvocationToken<RecPlay>>
        , IVisitor<IToken, IParserDirector, RefToken<TreeCaseSet>>
    {
        public void Visit(IParserDirector dir, BeginToken<TreeCaseSet> token)
        {
            var newCaseSetNode = new CaseSetNode(token.Owner);
            dir.AllCaseSets.Add(token.Owner, newCaseSetNode);
            dir.CurrentCaseSetNode = newCaseSetNode;
        }

        public void Visit(IParserDirector dir, EndToken<TreeCaseSet> token)
        {
            dir.CurrentCaseSetNode = null;
        }

        public void Visit(IParserDirector dir, InvocationToken<RecPlay> token)
        {
            ICodeLocation codeLocation = token.InvocationRecord.CodeLocation;
            IInvocation invocation = token.InvocationRecord.Invocation;

            if (dir.CurrentCaseSetNode == null)
            {
                throw new InvalidSyntaxException("Call must be performed within CaseSet definition block: {0}",
                    codeLocation.GetUserCodeInfo());
            }

            // Construct tree

            // CASE SETTER
            {
                PropertyCallKey setterCallKey
                    = InvocationUtil.TryGetPropertyCallKeyFromSetter(invocation);

                if (setterCallKey != null)
                {
                    INode parent = FindRecursivelyPlaceInTreeToAddPropertyCall(null, dir.CurrentCaseSetNode, setterCallKey);

                    var parentToAddTo = parent as IExtendableNode;

                    if(parentToAddTo == null)
                    {
                        throw new InvalidCaseRecordException("Can not attach case record to parent.\nCase record: {0}\nParent: {1}",
                                                             codeLocation, GetUserCodeInfoOrToString(parent));
                    }
                    
                    object argumentValue = invocation.GetArgumentValue(invocation.Arguments.Length - 1);

                    var newNode = new RecPlayInterfacePropertyNode(
                        token.Owner,
                        token.Owner.ContributorName,
                        setterCallKey,
                        argumentValue,
                        codeLocation);

                    var caseBranchNode = new CaseBranchNode(codeLocation, newNode);

                    parentToAddTo.AddChild(caseBranchNode);
                }
            }
        }

        public void Visit(IParserDirector dir, RefToken<TreeCaseSet> node)
        {
            //TODO JRG HERE CONTINUE dir.CurrentCaseSetNode 
        }

        private static INode FindRecursivelyPlaceInTreeToAddPropertyCall(INode parent, INode node, PropertyCallKey propertyCallKey)
        {
            // TODO: GENERALIZE RECURSION CHECK SO THAT IT DISPLATCHES CHECK TO ANOTHER VISITOR, TO ENABLE
            // INTRODUCTION OF OTHER NODES AS ALTERNATIVE TO ICaseBranchNode

            // if call to the same property already exists in current case-node, then fork the case into two (build a tree branch)
            var branch = node as ICaseBranchNode;
            if (branch != null)
            {
                var caseFact = branch.CaseFact as IRecPlayInterfacePropertyNode;
                if (caseFact == null)
                    throw new NotSupportedException("Case Fact is expected to be of type IRecPlayInterfacePropertyNode currently");

                if (Equals(caseFact.PropertyCallKey, propertyCallKey))
                    return parent;

                // end of case definition: Add property-call to the end of this case definition
                INode subLevel = branch.SubLevels.LastOrDefault();
                if (subLevel == null)
                    return node;

                // elsewhere recursion
                return FindRecursivelyPlaceInTreeToAddPropertyCall(node, subLevel, propertyCallKey);

            }

            // end of case definition: Add property-call to the end of this case definition
            INode lastChild = node.Children.LastOrDefault();
            if (lastChild == null)
                return node;

            // elsewhere recursion
            return FindRecursivelyPlaceInTreeToAddPropertyCall(node, lastChild, propertyCallKey);
        }

        private static INode FindRecursivelyCurrentLeaf(INode node)
        {
            // end of case definition: Add property-call to the end of this case definition
            INode lastChild = node.Children.LastOrDefault();
            if (lastChild == null)
                return node;

            // elsewhere recursion
            return FindRecursivelyCurrentLeaf(lastChild);
        }

        private static string GetUserCodeInfoOrToString(INode node)
        {
            var codeLocatedObject = node as ICodeLocatedObject;
            return (codeLocatedObject != null)
                ? codeLocatedObject.CodeLocation.GetUserCodeInfo()
                : node != null
                    ? node.ToString()
                    : "null";
        }
    }
}