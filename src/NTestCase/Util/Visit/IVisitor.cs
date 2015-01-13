namespace NTestCase.Util.Visit
{
    /// <summary>Used a marker, to lookup assemblies for Visitors (i.e. by using DI-container) </summary>
    public interface IVisitor
    {
    }

    /// <summary>Used for DI-Container initialization of related Directors</summary>
    // ReSharper disable once UnusedTypeParameter
    public interface IVisitor<in TDir, in TNod> : IVisitor
        where TDir : IDirector<TNod>
    {
    }

    /// <summary>
    /// Interface to implement, in order to define a visitor for a given director and a concrete TConcreteNode node type
    /// </summary>
    /// <typeparam name="TDir">the director it is bound to</typeparam>
    /// <typeparam name="TNod">the node upper class of the director</typeparam>
    /// <typeparam name="TConcreteNode">The concrete node type, the visitor is defined for</typeparam>
    public interface IVisitor<in TDir, in TNod, in TConcreteNode> : IVisitor<TDir, TNod> 
        where TDir : IDirector<TNod>
        where TConcreteNode : TNod
    {
        void Visit(TDir director, TConcreteNode node);
    }
}