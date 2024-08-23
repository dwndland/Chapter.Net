// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WorkingIndicatorView.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class WorkingIndicatorView
{
    public WorkingIndicatorView()
    {
        InitializeComponent();

        DataContext = new WorkingIndicatorViewModel();
    }
}