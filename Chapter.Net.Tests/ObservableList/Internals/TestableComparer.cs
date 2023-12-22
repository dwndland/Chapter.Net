// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TestableComparer.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

internal class TestableComparer : IComparer<int>
{
    public bool Triggered { get; private set; }

    public int Compare(int x, int y)
    {
        Triggered = true;
        return x.CompareTo(y);
    }
}