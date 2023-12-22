// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncDelegateCommandViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class AsyncDelegateCommandViewModel : ObservableObject
{
    private int _value;

    public AsyncDelegateCommandViewModel()
    {
        Value = 1;
        LowerCommand = new AsyncDelegateCommand(Lower);
        HigherCommand = new AsyncDelegateCommand(Higher);
    }

    public IDelegateCommand LowerCommand { get; }

    public IDelegateCommand HigherCommand { get; }

    public int Value
    {
        get => _value;
        private set => NotifyAndSetIfChanged(ref _value, value);
    }

    private async Task Lower()
    {
        await Task.Delay(1000);
        --Value;
    }

    private async Task Higher()
    {
        await Task.Delay(1000);
        ++Value;
    }
}