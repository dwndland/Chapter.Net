// -----------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExView.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class EnumerableExView
{
    public EnumerableExView()
    {
        InitializeComponent();

        DataContext = new EnumerableExViewModel();
    }
}