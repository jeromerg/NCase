using System.Collections.Generic;
using System.Linq;
using NTestCase.Util.Quality;

namespace NTestCase.Util.Typ
{
    public class TypeTopology
    {
        private readonly System.Type mType;
        private readonly Dictionary<System.Type, HashSet<System.Type>> mAllTypesWithChildren = new Dictionary<System.Type, HashSet<System.Type>>();

        public TypeTopology(System.Type type)
        {
            mType = type;
            mAllTypesWithChildren.Add(type, new HashSet<System.Type>());
            CollectAllParentTypes(type);
        }

        [CanBeNull]
        public System.Type ResolveBestUnambiguousTargetType(HashSet<System.Type> targetCandidates)
        {
            var excludedTypes = new List<TargetTypeInfo>();

            var concernedCandidates = new HashSet<System.Type>();
            foreach (var candidate in targetCandidates)
            {
                if (!mAllTypesWithChildren.ContainsKey(candidate))
                    excludedTypes.Add(new TargetTypeInfo(candidate, TargetTypeStatus.OutsideTypeTopology));
                else
                    concernedCandidates.Add(candidate);
            }

            foreach (var candidate in concernedCandidates)
            {
                bool contains = IsAnyTopologicalChildACandidate(candidate, concernedCandidates);
                if (contains)
                {
                    excludedTypes.Add(new TargetTypeInfo(candidate, TargetTypeStatus.ChildClassTakesPrecedence));
                    continue;
                }

                var parents = new HashSet<System.Type>(GetAllParents(candidate));

                System.Type candidateInClosure = candidate;
                if (concernedCandidates.Any(otherCandidate => otherCandidate != candidateInClosure && !parents.Contains(otherCandidate)))
                {
                    excludedTypes.Add(new TargetTypeInfo(candidate, TargetTypeStatus.AmbiguousMatch));
                    continue;
                }

                return candidate;
            }

            throw new TargetTypeNotResolvedException(mType, excludedTypes);
        }

        private void CollectAllParentTypes(System.Type type)
        {
            if (type.BaseType != null)
            {
                AddParentAndLink(type, type.BaseType);
            }

            foreach (var implementedInterface in type.GetInterfaces())
            {
                AddParentAndLink(type, implementedInterface);
            }
        }

        private void AddParentAndLink(System.Type type, System.Type parent)
        {
            HashSet<System.Type> parentsChildren;
            if (!mAllTypesWithChildren.TryGetValue(parent, out parentsChildren))
            {
                parentsChildren = new HashSet<System.Type>();
                mAllTypesWithChildren.Add(parent, parentsChildren);
            }

            parentsChildren.Add(type);

            CollectAllParentTypes(parent);
        }

        private IEnumerable<System.Type> GetAllParents(System.Type type)
        {
            if (type.BaseType != null)
            {
                yield return type.BaseType;
                foreach (var parentOfParent in GetAllParents(type.BaseType))
                    yield return parentOfParent;
            }

            foreach (var implementedInterface in type.GetInterfaces())
            {
                yield return implementedInterface;
                foreach (var parentOfParent in GetAllParents(implementedInterface))
                    yield return parentOfParent;
            }

        }

        private bool IsAnyTopologicalChildACandidate(System.Type type, HashSet<System.Type> candidates)
        {
            foreach (var child in mAllTypesWithChildren[type])
            {
                if (candidates.Contains(child))
                    return true;

                if (IsAnyTopologicalChildACandidate(child, candidates))
                    return true;
            }
            return false;
        }
    }
    

}
