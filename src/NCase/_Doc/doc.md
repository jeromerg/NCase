How-To
======

Add new Case Set
----------------

Example `ITree`

- Declare new interface `ITree` extending `ICaseSet`
    - The interface is used as following: `caseBuilder.CreateSet<ITree>("myTree")`
    - put interface into `Api`
- Create class `TreeCaseSet : ITree` 
    - Belongs to `Imp`
- Create class `TreeCaseSetFactory : ICaseSetFactory<ITree>`
    - Belongs to `Imp`
- Add `TreeCaseSetFactory` to IoC-Container
    - Autofac: `builder.RegisterType<TreeCaseSetFactory>().AsImplementedInterfaces().SingleInstance()`

- Implement a new parser visitor foreach token that are inserted by `TreeCaseSet`: 
    - `IParserVisitor<BeginToken<CaseSet.TreeCaseSet>>`
    - `IParserVisitor<EndToken<CaseSet.TreeCaseSet>>`
    - `IParserVisitor<RefToken<CaseSet.TreeCaseSet>>`


- Add interface `ICaseTreeNode : ICaseSetNode`
- Add implementation `CaseTreeNode : ICaseTreeNode`


