// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationView.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class ValidationView
{
    public ValidationView()
    {
        InitializeComponent();

        DataContext = new ValidationViewModel();
    }
}