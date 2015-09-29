using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCase.Front.Api;
using NVisitor.Api.Common;

namespace NCase.Front.Imp.Op
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

            // prepare the visit action and dispatcher it
            return (someDirector, operation1, car1) =>
                   visitMethod.Invoke(visitorInstance, new object[] {someDirector, operation1, car1});
        }
    }
}