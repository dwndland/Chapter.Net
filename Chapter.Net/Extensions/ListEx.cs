// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ListEx.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace Chapter.Net
{
    /// <summary>
    ///     Extends a <see cref="IList{T}" /> or <see cref="List{T}" /> with useful methods.
    /// </summary>
    public static class ListEx
    {
        /// <summary>
        ///     Gets the index of the first element matched by a condition.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to gets the index from.</param>
        /// <param name="condition">The check which item to return the index for.</param>
        /// <returns>The index of the first matched item; otherwise -1.</returns>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">condition cannot be null.</exception>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> condition)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            for (var i = 0; i < list.Count; ++i)
                if (condition(list[i]))
                    return i;

            return -1;
        }

        /// <summary>
        ///     Gets the index of the last element matched by a condition.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to gets the index from.</param>
        /// <param name="condition">The check which item to return the index for.</param>
        /// <returns>The index of the last matched item; otherwise -1.</returns>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">condition cannot be null.</exception>
        public static int LastIndexOf<T>(this IList<T> list, Func<T, bool> condition)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            for (var i = list.Count - 1; i >= 0; --i)
                if (condition(list[i]))
                    return i;
            return -1;
        }

        /// <summary>
        ///     Removes the first matching item.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to remove the item from.</param>
        /// <param name="filter">The check which item to remove.</param>
        /// <returns>True if an item got removed; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">filter cannot be null.</exception>
        public static bool RemoveFirst<T>(this IList<T> list, Predicate<T> filter)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            var position = list.IndexOf(x => filter(x));
            if (position == -1)
                return false;
            list.RemoveAt(position);
            return true;
        }

        /// <summary>
        ///     Removes the last matching item.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to remove the item from.</param>
        /// <param name="filter">The check which item to remove.</param>
        /// <returns>True if an item got removed; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">filter cannot be null.</exception>
        public static bool RemoveLast<T>(this IList<T> list, Predicate<T> filter)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            var position = list.LastIndexOf(x => filter(x));
            if (position == -1)
                return false;
            list.RemoveAt(position);
            return true;
        }

        /// <summary>
        ///     Adds all items to the collection where the filter matches.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to add the items to.</param>
        /// <param name="items">The items to add.</param>
        /// <param name="filter">The check to decide what items to add.</param>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">items cannot be null.</exception>
        /// <exception cref="ArgumentNullException">filter cannot be null.</exception>
        public static void AddRangeIf<T>(this List<T> list, IEnumerable<T> items, Predicate<T> filter)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            var itemsToAdd = items.Where(x => filter(x));
            list.AddRange(itemsToAdd);
        }

        /// <summary>
        ///     Adds all not null items to the collection.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to add the items to.</param>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">items cannot be null.</exception>
        public static void AddRangeIfNotNull<T>(this List<T> list, IEnumerable<T> items)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            list.AddRangeIf(items, x => x != null);
        }

        /// <summary>
        ///     Inserts the item if it matches.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to insert the item in.</param>
        /// <param name="index">The position where to insert at.</param>
        /// <param name="item">The item to insert.</param>
        /// <param name="matches">The check if the item is to insert.</param>
        /// <returns>True if the item got inserted; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">matches cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">index is out of range.</exception>
        public static bool InsertIf<T>(this IList<T> list, int index, T item, Predicate<T> matches)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (matches == null)
                throw new ArgumentNullException(nameof(matches));

            if (matches(item))
            {
                list.Insert(index, item);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Inserts the item if its not null.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to insert the item in.</param>
        /// <param name="index">The position where to insert at.</param>
        /// <param name="item">The item to insert.</param>
        /// <returns>True if the item got inserted; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">matches cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">index is out of range.</exception>
        public static bool InsertIfNotNull<T>(this IList<T> list, int index, T item)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            return list.InsertIf(index, item, x => x != null);
        }

        /// <summary>
        ///     Inserts all items to the collection where the filter matches.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to insert the items in.</param>
        /// <param name="index">The position where to insert at.</param>
        /// <param name="items">The items to insert.</param>
        /// <param name="filter">The filter to decide if the item is to add.</param>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">items cannot be null.</exception>
        /// <exception cref="ArgumentNullException">filter cannot be null.</exception>
        public static void InsertRangeIf<T>(this List<T> list, int index, IEnumerable<T> items, Predicate<T> filter)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            var itemsToInsert = items.Where(x => filter(x));
            list.InsertRange(index, itemsToInsert);
        }

        /// <summary>
        ///     Inserts all not null items to the collection.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="list">The list to insert the items in.</param>
        /// <param name="index">The position where to insert at.</param>
        /// <param name="items">The items to insert.</param>
        /// <exception cref="ArgumentNullException">list cannot be null.</exception>
        /// <exception cref="ArgumentNullException">items cannot be null.</exception>
        public static void InsertRangeIfNotNull<T>(this List<T> list, int index, IEnumerable<T> items)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            list.InsertRangeIf(index, items, x => x != null);
        }
    }
}