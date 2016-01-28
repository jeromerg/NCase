using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NUtil.Linq
{
    public static class CascadeExtensions
    {
        public static T CascadeAdd<TKey, T>(
            [NotNull] this Dictionary<TKey, T> dict, 
            [NotNull] TKey key)
            where T : new()
        {
            if (dict == null) throw new ArgumentNullException("dict");
            if (key == null) throw new ArgumentNullException("key");

            T t;
            if (dict.TryGetValue(key, out t))
                return t;

            t = new T();
            dict.Add(key, t);
            return t;
        }

        public static IEnumerable<T> CascadeRemove<TKey, T>(
            [NotNull] this Dictionary<TKey, T> dict,
            [NotNull] TKey key)
            where T : IEnumerable
        {
            if (dict == null) throw new ArgumentNullException("dict");

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

        public static bool CascadeRemove<TVal>(
            [NotNull, ItemNotNull] this IEnumerable<ICollection<TVal>> collectionEnumerable,
            TVal value)
        {
            bool removed = false;
            foreach (ICollection<TVal> set in collectionEnumerable)
                removed |= set.Remove(value);

            return removed;
        }

        public static T CascadeTryFirst<TKey, T>(
            [CanBeNull] this Dictionary<TKey, T> dict,
            out TKey key,
            TKey fallbackKey = default(TKey))
        {
            if (dict == null)
            {
                key = fallbackKey;
                return default(T);
            }

            if (!dict.Any())
            {
                key = fallbackKey;
                return default(T);
            }

            KeyValuePair<TKey, T> pair = dict.First();
            key = pair.Key;
            return pair.Value;
        }

        public static bool CascadeTryFirst<TKey>(
            [CanBeNull] this ICollection<TKey> collection,
            out TKey key,
            TKey fallbackKey = default(TKey))
        {
            if (collection == null)
            {
                key = fallbackKey;
                return false;
            }

            if (!collection.Any())
            {
                key = fallbackKey;
                return false;
            }

            key = collection.First();
            return true;
        }

        public static T CascadeGetOrDefault<TKey, T>(
            [CanBeNull] this Dictionary<TKey, T> dict,
            [NotNull] TKey key)
        {
            if (key == null) throw new ArgumentNullException("key");

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