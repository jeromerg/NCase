How-To
======

Add new Case Set
----------------

Example `ITree`

- Declare new interface `ITree` extending `ICaseSet`
    - The name will be used as following: `caseBuilder.CreateSet<ITree>("myTree")`, so use a short name
    - Belongs to `Api`
- Create class `TreeCaseSet : ITree` 
    - Belongs to `Imp`
- Create class `TreeCaseSetFactory : ICaseSetFactory<ITree>`
    - Belongs to `Imp`
- Add `TreeCaseSetFactory` to IoC-Container
    - Autofac: `builder.RegisterType<CardinalProductCaseSetFactory>().AsImplementedInterfaces().InstancePerDependency()`
- Add `TreeCaseSetFactory` to IoC-Container

- Implement a new parser visitor foreach token that are inserted by `TreeCaseSet`: 
    - `IVisitor<IToken, IParseDirector, BeginToken<CaseSet.TreeCaseSet>>`
    - `IVisitor<IToken, IParseDirector, EndToken<CaseSet.TreeCaseSet>>`
    - `IVisitor<IToken, IParseDirector, RefToken<CaseSet.TreeCaseSet>>`


- Add `ICaseTreeNode : ICaseSetNode`
- Add `CaseTreeNode : ICaseTreeNode`


