// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableObservableObjectView.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class ValidatableObservableObjectView
{
    public ValidatableObservableObjectView()
    {
        InitializeComponent();

        DataContext = new ValidatableObservableObjectViewModel();
    }
}