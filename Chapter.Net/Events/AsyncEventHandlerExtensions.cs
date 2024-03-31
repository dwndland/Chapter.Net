// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncEventHandlerExtensions.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Chapter.Net
{
    /// <summary>
    ///     Brings extensions to the <see cref="AsyncEventHandler" />.
    /// </summary>
    public static class AsyncEventHandlerExtensions
    {
        /// <summary>
        ///     Invokes all async event handlers.
        /// </summary>
        /// <param name="handler">The handler which handler to invoke.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        /// <returns>The task to await all event handlers.</returns>
        public static Task InvokeAll(this AsyncEventHandler handler, object sender, EventArgs e)
        {
            return Task.WhenAll(handler.GetEventHandlers().Select(handleAsync => handleAsync(sender, e)));
        }

        /// <summary>
        ///     Invokes all async event handlers.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event args.</typeparam>
        /// <param name="handler">The handler which handler to invoke.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        /// <returns>The task to await all event handlers.</returns>
        public static Task InvokeAll<TEventArgs>(this AsyncEventHandler<TEventArgs> handler, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            return Task.WhenAll(handler.GetEventHandlers().Select(handleAsync => handleAsync(sender, e)));
        }

        /// <summary>
        ///     Returns all async event handlers.
        /// </summary>
        /// <param name="handler">The handler which handler to read.</param>
        /// <returns>The list of event handlers.</returns>
        public static IEnumerable<AsyncEventHandler> GetEventHandlers(this AsyncEventHandler handler)
        {
            return handler.GetInvocationList().Cast<AsyncEventHandler>();
        }

        /// <summary>
        ///     Returns all async event handlers.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event args.</typeparam>
        /// <param name="handler">The handler which handler to read.</param>
        /// <returns>The list of event handlers.</returns>
        public static IEnumerable<AsyncEventHandler<TEventArgs>> GetEventHandlers<TEventArgs>(this AsyncEventHandler<TEventArgs> handler) where TEventArgs : EventArgs
        {
            return handler.GetInvocationList().Cast<AsyncEventHandler<TEventArgs>>();
        }
    }
}