// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IInvokator.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Provides a way how to invoke an action.
/// </summary>
public interface IInvokator
{
    /// <summary>
    ///     Invokes an action.
    /// </summary>
    /// <param name="action">The action to invoke.</param>
    void Invoke(Action action);
}