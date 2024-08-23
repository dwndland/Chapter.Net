// -----------------------------------------------------------------------------------------------------------------
// <copyright file="EventReceiver.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class EventReceiver : ObservableObject, IDisposable
{
    private readonly AsyncEventHandlerViewModel _sender1;
    private readonly EventArgsViewModel _sender2;
    private int _item1;
    private int _item2;

    public EventReceiver()
    {
    }

    public EventReceiver(AsyncEventHandlerViewModel sender1)
    {
        _sender1 = sender1;
        sender1.DemoEvent += OnSender1DemoEvent;
    }

    public EventReceiver(EventArgsViewModel sender2)
    {
        _sender2 = sender2;
        _sender2.DemoEvent += OnSender2DemoEvent;
    }

    public int Item1
    {
        get => _item1;
        private set => NotifyAndSetIfChanged(ref _item1, value);
    }

    public int Item2
    {
        get => _item2;
        private set => NotifyAndSetIfChanged(ref _item2, value);
    }

    public void Dispose()
    {
        if (_sender1 != null)
            _sender1.DemoEvent -= OnSender1DemoEvent;
        if (_sender2 != null)
            _sender2.DemoEvent -= OnSender2DemoEvent;
    }

    private async Task OnSender1DemoEvent(object sender, EventArgs e)
    {
        await Task.Delay(1000);
        Item1 += 1;
        Item2 += 1;
    }

    private void OnSender2DemoEvent(object sender, EventArgs<int, int> e)
    {
        Item1 += e.Item1;
        Item2 += e.Item2;
    }
}