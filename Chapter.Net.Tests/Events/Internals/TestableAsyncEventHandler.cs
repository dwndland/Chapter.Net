﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TestableAsyncEventHandler.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

internal class TestableAsyncEventHandler
{
    public AsyncEventHandler<MyEventArgs> TestEventWithEventArgs;

    public AsyncEventHandler TestEventWithoutEventArgs;
}