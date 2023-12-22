// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateCommandView.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class DelegateCommandView
{
    public DelegateCommandView()
    {
        InitializeComponent();

        DataContext = new DelegateCommandViewModel();
    }
}