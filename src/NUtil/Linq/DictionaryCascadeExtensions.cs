using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NUtil.Linq
{
    public static class DictionaryCascadeExtensions
    {
        public static T CascadeAdd<TKey, T>([NotNull] this Dictionary<TKey, T> dict, [NotNull] TKey key) 
            where T : new()
        {
            if (key == null) throw new ArgumentNullException("key");

            T t;
            if (dict.TryGetValue(key, out t)) 
                return t;

            t = new T();
            dict.Add(key, t);
            return t;
        }

        public static void CascadeAdd<T>([NotNull] this HashSet<T> hasSet, T item)
        {
            hasSet.Add(item);
        }

        public static IEnumerable<T> CascadeRemove<TKey, T>([CanBeNull] this Dictionary<TKey, T> dict, TKey key)
            where T : IEnumerable
        {
            return CascadeRemove(Enumerable.Repeat(dict, 1), key);
        }

        public static IEnumerable<T> CascadeRemove<TKey, T>(
            [NotNull, ItemNotNull] this IEnumerable<Dictionary<TKey, T>> dictEnumerable,
            [NotNull] TKey key)
            where T : IEnumerable
        {
            if (key == null) throw new ArgumentNullException("key");

            foreach (Dictionary<TKey, T> dict in dictEnumerable)
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

        public static void CascadeRemove<TKey>([NotNull, ItemNotNull] this IEnumerable<HashSet<TKey>> hashSetEnumerable, TKey key)
        {
            foreach (HashSet<TKey> set in hashSetEnumerable)
                set.Remove(key);
        }

        public static T CascadeFirst<T>([CanBeNull] this Dictionary<int, T> dict, out int key)
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

        public static bool CascadeFirst([CanBeNull] this HashSet<int> hashSet, out int key)
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

        public static T CascadeGet<T>([CanBeNull] this Dictionary<int, T> dict, int key)
        {
            if (dict == null)
                return default(T);

            if (!dict.Any())
                return default(T);

            T next;
            dict.TryGetValue(key, out next);

            return next;
        }
    }
}