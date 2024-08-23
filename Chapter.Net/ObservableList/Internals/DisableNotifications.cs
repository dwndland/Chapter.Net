// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DisableNotifications.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

internal class DisableNotifications : IDisposable
{
    public void Dispose()
    {
        Disposed?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler Disposed;
}