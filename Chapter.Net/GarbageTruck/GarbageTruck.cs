// -----------------------------------------------------------------------------------------------------------------
// <copyright file="GarbageTruck.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Holds a list of <see cref="IDisposable" />s for an easy dispose of all of them in one go.
/// </summary>
public class GarbageTruck : IDisposable
{
    private readonly List<IDisposable> _disposables;

    /// <summary>
    ///     Creates a new instance of GarbageTruck.
    /// </summary>
    public GarbageTruck()
    {
        _disposables = new List<IDisposable>();
    }

    /// <summary>
    ///     Disposes all currently hold items.
    /// </summary>
    public void Dispose()
    {
        var itemsToDispose = _disposables.ToList();
        _disposables.Clear();
        itemsToDispose.ForEach(x => x.Dispose());
    }

    /// <summary>
    ///     Adds a new disposable to the truck.
    /// </summary>
    /// <param name="disposable">The item to keep to dispose later.</param>
    /// <exception cref="ArgumentNullException">disposable cannot be null.</exception>
    public void Add(IDisposable disposable)
    {
        if (disposable == null)
            throw new ArgumentNullException(nameof(disposable));

        _disposables.Add(disposable);
    }
}