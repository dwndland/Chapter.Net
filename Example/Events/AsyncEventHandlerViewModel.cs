// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncEventHandlerViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class AsyncEventHandlerViewModel
{
    public AsyncEventHandlerViewModel()
    {
        SendAsyncEventCommand = new AsyncDelegateCommand(SendAsyncEvent);
        EventReceiver = new EventReceiver(this);
    }

    public IDelegateCommand SendAsyncEventCommand { get; }

    public EventReceiver EventReceiver { get; }

    public event AsyncEventHandler DemoEvent;

    private async Task SendAsyncEvent()
    {
        if (DemoEvent != null)
            await DemoEvent.Invoke(this, new EventArgs<int, int>(1, 2));
    }
}