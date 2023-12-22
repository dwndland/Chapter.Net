// -----------------------------------------------------------------------------------------------------------------
// <copyright file="GarbageTruckViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class GarbageTruckViewModel : IDisposable
{
    private readonly GarbageTruck _garbageTruck;

    public GarbageTruckViewModel()
    {
        _garbageTruck = new GarbageTruck();

        _garbageTruck.Add(new EventReceiver());
        _garbageTruck.Add(new EventReceiver());
        _garbageTruck.Add(new EventReceiver());
    }

    public void Dispose()
    {
        _garbageTruck.Dispose();
    }
}