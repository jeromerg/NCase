namespace NUtil.Generics
{
    public interface IHolder<T>
    {
        T Value { get; set; }
    }
}