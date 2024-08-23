// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateCommandViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class DelegateCommandViewModel : ObservableObject
{
    private int _value;

    public DelegateCommandViewModel()
    {
        Value = 1;
        LowerCommand = new DelegateCommand(Lower);
        HigherCommand = new DelegateCommand(Higher);
    }

    public IDelegateCommand LowerCommand { get; }

    public IDelegateCommand HigherCommand { get; }

    public int Value
    {
        get => _value;
        private set => NotifyAndSetIfChanged(ref _value, value);
    }

    private void Lower()
    {
        --Value;
    }

    private void Higher()
    {
        ++Value;
    }
}