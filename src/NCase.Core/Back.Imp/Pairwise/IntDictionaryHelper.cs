using System.Collections.Generic;
using JetBrains.Annotations;

namespace NCase.Back.Imp.Pairwise
{
    public static class IntDictionaryHelper
    {
        public static T GetOrCreate<T>([NotNull] this Dictionary<int, T> dict, int key) where T : new()
        {
            T t;
            if (!dict.TryGetValue(key, out t))
            {
                t = new T();
                dict.Add(key, t);
            }
            return t;
        }

        public static T GetOrDefault<T>([CanBeNull] this Dictionary<int, T> dict, int key) where T : class, new()
        {
            if (dict == null)
                return null;

            T t;
            return !dict.TryGetValue(key, out t) ? null : t;
        }

    }
}