using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static IEnumerable<T> CascadeClean<T>([CanBeNull] this Dictionary<int, T> dict, int key)
            where T : IEnumerable
        {
            return CascadeClean(Enumerable.Repeat(dict, 1), key);
        }

        public static IEnumerable<T> CascadeClean<T>([NotNull] this IEnumerable<Dictionary<int, T>> dictEnumerable, int key)
            where T : IEnumerable
        {
            foreach (var dict in dictEnumerable)
            {
                T t;
                if (!dict.TryGetValue(key, out t))
                    yield break;

                yield return t;

                bool any = t.GetEnumerator().MoveNext();
                if (!any)
                    dict.Remove(key);
            }
        }

        public static void Remove<T>([NotNull] this IEnumerable<HashSet<T>> hashSetEnumerable, T key)
        {
            foreach (HashSet<T> set in hashSetEnumerable)
                set.Remove(key);
        }

        public static T CascadeFirst<T>([NotNull] this Dictionary<int, T> dict, out int key)
        {
            KeyValuePair<int, T> pair = dict.First();
            key = pair.Key;
            return pair.Value;
        }

        public static void CascadeFirst([NotNull] this HashSet<int> hashSet, out int key)
        {
            key = hashSet.First();
        }
    }
}