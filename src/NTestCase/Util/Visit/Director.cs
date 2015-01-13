using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NTestCase.Util.Quality;
using NTestCase.Util.Typ;

namespace NTestCase.Util.Visit
{
    public abstract class Director<TDir, TNod> : IDirector<TNod>
        where TDir : IDirector<TNod>
    {

        private readonly Dictionary<Type, IVisitor<TDir, TNod>> mVisitorsByNodeType = new Dictionary<Type, IVisitor<TDir, TNod>>();

        private readonly Dictionary<Type, Type> mNearestVisitorNodeTypeCache = new Dictionary<Type, Type>(); 

        protected Director(IEnumerable<IVisitor<TDir, TNod>> visitors)
        {
            foreach (var visitor in visitors)
            {
                IEnumerable<Type> implementedVisitorTypes 
                    = visitor.GetType().GetInterfaces()
                                       .Where(interfaceType => interfaceType.IsGenericType
                                                  && interfaceType.GetGenericTypeDefinition() == typeof(IVisitor<,,>));

                foreach (var implementedVisitorType in implementedVisitorTypes)
                {
                    // remark: IVisitor<in TDir, in TNod, in TConcreteNode>

                    Type concreteNodeType = implementedVisitorType.GetGenericArguments()[2];

                    IVisitor<TDir, TNod> conflictingVisitor;
                    if (mVisitorsByNodeType.TryGetValue(concreteNodeType, out conflictingVisitor))
                    {
                        string msg = string.Format("Two visitors apply for the same Node type: {0} and {1}",
                            conflictingVisitor.GetType().Name, visitor.GetType().Name);
                        throw new ArgumentException(msg);
                    }

                    mVisitorsByNodeType.Add(concreteNodeType, visitor);
                }
            }
        }

        public virtual void Visit([NotNull] TNod node)
        {
            if (ReferenceEquals(node, null))
                throw new ArgumentNullException("node");

            Type nearestVisitorNodeType = FindNearestVisitorNodeType(node.GetType());

            Type bestVisitorType =
                typeof(IVisitor<,,>).MakeGenericType(typeof(TDir), typeof(TNod), nearestVisitorNodeType);
 
            IVisitor<TDir, TNod> bestVisitor = mVisitorsByNodeType[nearestVisitorNodeType];
            
            MethodInfo visitMethod = bestVisitorType.GetMethod("Visit");

            visitMethod.Invoke(bestVisitor, new object[] {this, node});
        }

        [NotNull]
        private Type FindNearestVisitorNodeType(Type nodeType)
        {
            Type nearestVisitorNodeType;
            if (!mNearestVisitorNodeTypeCache.TryGetValue(nodeType, out nearestVisitorNodeType))
            {
                var typeTopology = new TypeTopology(nodeType);
                nearestVisitorNodeType = typeTopology
                    .ResolveBestUnambiguousTargetType(new HashSet<Type>(mVisitorsByNodeType.Keys));
            }
            return nearestVisitorNodeType;
        }
    }
}