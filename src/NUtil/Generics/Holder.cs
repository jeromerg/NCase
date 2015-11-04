namespace NUtil.Generics
{
    public class Holder<T> : IHolder<T>
    {
        public T Value { get; set; }
    }
}