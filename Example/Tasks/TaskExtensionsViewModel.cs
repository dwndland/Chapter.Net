// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TaskExtensionsViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class TaskExtensionsViewModel : ObservableObject
{
    private int _value;

    public TaskExtensionsViewModel()
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
        Update(-2).FireAndForget();
        --Value;
    }

    private void Higher()
    {
        Update(2).FireAndForget();
        ++Value;
    }

    private async Task Update(int value)
    {
        await Task.Delay(1000);
        Value += value;
    }
}