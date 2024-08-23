﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocatorView.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class ServiceLocatorView
{
    public ServiceLocatorView()
    {
        InitializeComponent();

        DataContext = new ServiceLocatorViewModel();
    }
}