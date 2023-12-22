﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableListView.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class ObservableListView
{
    public ObservableListView()
    {
        InitializeComponent();

        DataContext = new ObservableListViewModel();
    }
}