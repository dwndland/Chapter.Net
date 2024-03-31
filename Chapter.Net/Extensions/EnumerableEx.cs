// -----------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableEx.cs" company="my-libraries">
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
    ///     Provides additional actions on an <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableEx
    {
        /// <summary>
        ///     Provides a way to repeat a func in a sequence.
        /// </summary>
        /// <typeparam name="TResult">The inner type of the <see cref="IEnumerable{T}" />.</typeparam>
        /// <param name="elementCallback">The callback to execute for each element.</param>
        /// <param name="count">The expected amount of elements in the collection.</param>
        /// <returns>A new <see cref="IEnumerable{T}" />.</returns>
        /// <exception cref="ArgumentNullException">elementCallback cannot be null.</exception>
        public static IEnumerable<TResult> Repeat<TResult>(Func<TResult> elementCallback, uint count)
        {
            if (elementCallback == null)
                throw new ArgumentNullException(nameof(elementCallback));

            for (var i = 0; i < count; ++i)
                yield return elementCallback();
        }

        /// <summary>
        ///     Executes an action for each entry of an <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">The inner type of the <see cref="IEnumerable{T}" />.</typeparam>
        /// <param name="elements">The collection to loop.</param>
        /// <param name="action">The action to call for each element.</param>
        /// <exception cref="ArgumentNullException">elements cannot be null.</exception>
        /// <exception cref="ArgumentNullException">action cannot be null.</exception>
        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var element in elements)
                action(element);
        }

        /// <summary>
        ///     Shuffles the elements.
        /// </summary>
        /// <typeparam name="T">The inner type of the <see cref="IEnumerable{T}" />.</typeparam>
        /// <param name="elements">The collection to shuffle.</param>
        /// <returns>A new <see cref="IEnumerable{T}" /> with the elements shuffled.</returns>
        /// <exception cref="ArgumentNullException">elements cannot be null.</exception>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            return elements.OrderBy(_ => Guid.NewGuid());
        }
    }
}