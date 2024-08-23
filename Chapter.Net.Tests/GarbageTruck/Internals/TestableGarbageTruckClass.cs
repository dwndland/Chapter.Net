// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TestableGarbageTruckClass.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

internal class TestableGarbageTruckClass : IDisposable
{
    public bool IsDisposed { get; set; }

    public void Dispose()
    {
        IsDisposed = true;
    }
}