// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TestableInvokator.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

internal class TestableInvokator : IInvokator
{
    public bool Triggered { get; private set; }

    public void Invoke(Action action)
    {
        Triggered = true;
        action();
    }
}