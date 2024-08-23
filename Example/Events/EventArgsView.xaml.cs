// -----------------------------------------------------------------------------------------------------------------
// <copyright file="EventArgsView.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class EventArgsView
{
    public EventArgsView()
    {
        InitializeComponent();

        DataContext = new EventArgsViewModel();
    }
}