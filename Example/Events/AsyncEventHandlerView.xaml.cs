// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncEventHandlerView.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class AsyncEventHandlerView
{
    public AsyncEventHandlerView()
    {
        InitializeComponent();

        DataContext = new AsyncEventHandlerViewModel();
    }
}