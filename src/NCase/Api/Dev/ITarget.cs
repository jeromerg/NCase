namespace NTestCase.Api.Dev
{
    /// <summary>
    /// Marker interface, to mark classes, that identify a component and 
    /// a target on the component (ie. instance + propertyCall).
    /// Used for its Equals and GetHashCode implementation to identify 
    /// unique component+target on test case components 
    /// </summary>
    public interface ITarget
    {
    }
}