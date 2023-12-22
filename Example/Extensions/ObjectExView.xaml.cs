// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExView.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class ObjectExView
{
    public ObjectExView()
    {
        InitializeComponent();

        DataContext = new ObjectExViewModel();
    }
}