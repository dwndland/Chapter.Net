// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TaskExtensionsView.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class TaskExtensionsView
{
    public TaskExtensionsView()
    {
        InitializeComponent();

        DataContext = new TaskExtensionsViewModel();
    }
}