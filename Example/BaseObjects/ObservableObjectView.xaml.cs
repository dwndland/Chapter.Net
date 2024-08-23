// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableObjectView.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class ObservableObjectView
{
    public ObservableObjectView()
    {
        InitializeComponent();

        DataContext = new ObservableObjectViewModel();
    }
}