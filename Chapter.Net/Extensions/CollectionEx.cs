// -----------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionEx.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace

namespace Chapter.Net
{
    /// <summary>
    ///     Extends a <see cref="ICollection{T}" /> with useful methods.
    /// </summary>
    public static class CollectionEx
    {
        /// <summary>
        ///     Dynamically adds the item to the list if the given predicate matches.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="collection">The collection to add the item to.</param>
        /// <param name="item">The item to add.</param>
        /// <param name="matches">The match to decide if to add the item or not.</param>
        /// <returns>True if the item got added; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">collection cannot be null.</exception>
        /// <exception cref="ArgumentNullException">matches cannot be null.</exception>
        public static bool AddIf<T>(this ICollection<T> collection, T item, Predicate<T> matches)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (matches == null)
                throw new ArgumentNullException(nameof(matches));

            if (matches(item))
            {
                collection.Add(item);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Dynamically adds the item to the list if the given item is not null.
        /// </summary>
        /// <typeparam name="T">The inner type of the list.</typeparam>
        /// <param name="collection">The collection to add the item to.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item got added; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">collection cannot be null.</exception>
        public static bool AddIfNotNull<T>(this ICollection<T> collection, T item)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return collection.AddIf(item, x => x != null);
        }
    }
}