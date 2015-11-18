using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NUtil.Linq
{
    public static class DictionaryCascadeExtensions
    {
        public static T CascadeAdd<T>([NotNull] this Dictionary<int, T> dict, int key) where T : new()
        {
            T t;
            if (!dict.TryGetValue(key, out t))
            {
                t = new T();
                dict.Add(key, t);
            }
            return t;
        }

        public static void CascadeAdd([NotNull] this HashSet<int> hasSet, int key)
        {
            hasSet.Add(key);
        }

        public static IEnumerable<T> CascadeRemove<T>([CanBeNull] this Dictionary<int, T> dict, int key)
            where T : IEnumerable
        {
            return CascadeRemove(Enumerable.Repeat(dict, 1), key);
        }

        public static IEnumerable<T> CascadeRemove<T>([NotNull, ItemNotNull] this IEnumerable<Dictionary<int, T>> dictEnumerable, int key)
            where T : IEnumerable
        {
            foreach (Dictionary<int, T> dict in dictEnumerable)
            {
                T t;
                if (!dict.TryGetValue(key, out t))
                    yield break;

                yield return t;

                // ReSharper disable once PossibleNullReferenceException
                bool any = t.GetEnumerator().MoveNext();
                if (!any)
                    dict.Remove(key);
            }
        }

        public static void CascadeRemove<T>([NotNull, ItemNotNull] this IEnumerable<HashSet<T>> hashSetEnumerable, T key)
        {
            foreach (HashSet<T> set in hashSetEnumerable)
                set.Remove(key);
        }

        public static T CascadeFirstOut<T>([CanBeNull] this Dictionary<int, T> dict, out int key)
        {
            if (dict == null)
            {
                key = -1;
                return default(T);
            }

            if (!dict.Any())
            {
                key = -1;
                return default(T);
            }

            KeyValuePair<int, T> pair = dict.First();
            key = pair.Key;
            return pair.Value;
        }

        public static bool CascadeFirstOut([CanBeNull] this HashSet<int> hashSet, out int key)
        {
            if (hashSet == null)
            {
                key = -1;
                return false;
            }

            if (!hashSet.Any())
            {
                key = -1;
                return false;
            }

            key = hashSet.First();
            return true;
        }

        public static T CascadeFirst<T>([CanBeNull] this Dictionary<int, T> dict, int key)
        {
            if (dict == null)
                return default(T);

            if (!dict.Any())
                return default(T);

            T next;
            dict.TryGetValue(key, out next);

            return next;
        }

        public static bool CascadeFirst([CanBeNull] this HashSet<int> hashSet, int key)
        {
            if (hashSet == null)
                return false;

            if (!hashSet.Any())
                return false;

            return hashSet.Contains(key);
        }
    }
}