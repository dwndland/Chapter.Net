// -----------------------------------------------------------------------------------------------------------------
// <copyright file="GarbageTruckView.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class GarbageTruckView
{
    public GarbageTruckView()
    {
        InitializeComponent();

        DataContext = new GarbageTruckViewModel();
    }
}