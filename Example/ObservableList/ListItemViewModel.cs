// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ListItemViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class ListItemViewModel : ObservableObject
{
    private string _name;

    public ListItemViewModel(string name)
    {
        _name = name;
    }

    public string Name
    {
        get => _name;
        set => NotifyAndSetIfChanged(ref _name, value);
    }
}