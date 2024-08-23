// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncDelegateCommandView.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class AsyncDelegateCommandView
{
    public AsyncDelegateCommandView()
    {
        InitializeComponent();

        DataContext = new AsyncDelegateCommandViewModel();
    }
}