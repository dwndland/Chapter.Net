// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncEventHandler.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Provides async events.
/// </summary>
/// <param name="sender">The event sender.</param>
/// <param name="e">The event args.</param>
/// <returns>The task to await.</returns>
public delegate Task AsyncEventHandler(object sender, EventArgs e);

/// <summary>
///     Provides async events.
/// </summary>
/// <typeparam name="TEventArgs">The type of event args.</typeparam>
/// <param name="sender">The event sender.</param>
/// <param name="e">The event args.</param>
/// <returns>The task to await.</returns>
public delegate Task AsyncEventHandler<in TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;