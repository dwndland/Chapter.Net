// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DirectInvokator.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Invokes actions directly.
/// </summary>
public sealed class DirectInvokator : IInvokator
{
    /// <summary>
    ///     Invokes an action.
    /// </summary>
    /// <param name="action">The action to invoke.</param>
    /// <exception cref="ArgumentNullException">action cannot be null.</exception>
    public void Invoke(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        action();
    }
}