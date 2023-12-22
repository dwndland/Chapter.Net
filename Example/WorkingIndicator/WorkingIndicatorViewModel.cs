// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WorkingIndicatorViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class WorkingIndicatorViewModel : ObservableObject
{
    private WorkingIndicator _workingIndicator;

    public WorkingIndicatorViewModel()
    {
        TestCommand = new AsyncDelegateCommand(Test);
    }

    public bool IsActive => WorkingIndicator.IsActive(_workingIndicator);

    public IDelegateCommand TestCommand { get; }

    private async Task Test()
    {
        using (_workingIndicator = new WorkingIndicator())
        {
            NotifyPropertyChanged(nameof(IsActive));
            await Task.Delay(1000);
        }

        NotifyPropertyChanged(nameof(IsActive));
    }
}