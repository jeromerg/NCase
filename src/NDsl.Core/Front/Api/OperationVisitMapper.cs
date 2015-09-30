using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NDsl.Front.Api;
using NVisitor.Api.Common;

namespace NDsl.Front.Imp.Op
{
    public class OperationVisitMapper
        : PairVisitMapperBase<
              IOp,
              IArtefactImp,
              IOperationVisitorClass,
              Func<IOperationDirector, IOp, IArtefactImp, object>>,
          IOperationVisitMapper
    {
        public OperationVisitMapper(IEnumerable<IOperationVisitorClass> visitors)
            : base(visitors, typeof (IOperationImp<,,,>), 1, 2)
        {
        }

        protected override Func<IOperationDirector, IOp, IArtefactImp, object>
            CreateVisitDelegate(
            IOp operation,
            IArtefactImp artefactImp,
            Type operationType,
            Type carType,
            IOperationVisitorClass visitorInstance,
            Func<IOperationDirector, IOp, IArtefactImp, object> directorDelegate)
        {
            List<Type> operationOpenTypes = operation
                .GetType()
                .GetInterfaces()
                .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof (IOp<,>))
                .ToList();

            if (operationOpenTypes.Count != 1)
                throw new ArgumentException(string.Format("Type {0} must implement IOp once and only once",
                                                          operation.GetType().FullName));

            Type artefactType = operationOpenTypes[0].GetGenericArguments()[0];
            Type resultType = operationOpenTypes[0].GetGenericArguments()[1];

            Type visitorClosedType = typeof (IOperationImp<,,,>)
                .MakeGenericType(artefactType, operationType, carType, resultType);

            // find the visit method in the closed generic type of the visitor
            MethodInfo visitMethod = visitorClosedType.GetMethod("Perform");

            if (visitMethod == null)
                throw new ArgumentException(string.Format("Perform method not found on closed interface {0}",
                                                          visitorClosedType.FullName));

            // prepare the visit action and dispatcher it
            return (someDirector, operation1, artefactImp1) =>
                   InvokeWithExplicitError(visitMethod, visitorInstance, new object[] {someDirector, operation1, artefactImp1});
        }

        private object InvokeWithExplicitError(MethodInfo method, object instance, object[] arguments)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            try
            {
                return method.Invoke(instance, arguments);
            }
            catch (ArgumentException e)
            {
                List<Type> expectedTypes = method.GetParameters().Select(p => p.ParameterType).ToList();
                List<Type> actualTypes = arguments.Select(p => p != null ? p.GetType() : null).ToList();

                if (expectedTypes.Count != actualTypes.Count)
                {
                    string msg = string.Format("Method invocation '{0}': expected {1} arguments, but passed {2}",
                                               method.Name,
                                               expectedTypes.Count,
                                               actualTypes.Count);

                    throw new ArgumentException(msg, e);
                }
                else
                {
                    var b = new StringBuilder();
                    for (int i = 0; i < expectedTypes.Count; i++)
                    {
                        Type expected = expectedTypes[i];
                        Type actual = actualTypes[i];
                        if (!expected.IsAssignableFrom(actual))
                            b.AppendFormat("parameter #{0}: {1} is not assignable to {2}", i, actual.FullName, expected.FullName);
                    }
                    string msg = string.Format("Method invocation '{0}': argument mismatch: {1}", method.Name, b);
                    throw new ArgumentException(msg, e);
                }
            }
            catch (TargetException e)
            {
                Type expectedTargetType = method.DeclaringType;
                Type actualTargetType = instance.GetType();

                string msg = string.Format("Method invocation '{0}': target mismatch: actual {1} is not assignable to {2}",
                                           method.Name,
                                           actualTargetType.FullName,
                                           expectedTargetType.FullName);

                throw new ArgumentException(msg, e);
            }
        }
    }
}