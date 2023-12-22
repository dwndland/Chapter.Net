// -----------------------------------------------------------------------------------------------------------------
// <copyright file="EventArgsViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class EventArgsViewModel
{
    public EventArgsViewModel()
    {
        SendEventCommand = new DelegateCommand(SendEvent);
        EventReceiver = new EventReceiver(this);
    }

    public IDelegateCommand SendEventCommand { get; }

    public EventReceiver EventReceiver { get; }

    public event EventHandler<EventArgs<int, int>> DemoEvent;

    private void SendEvent()
    {
        DemoEvent?.Invoke(this, new EventArgs<int, int>(1, 1));
    }
}