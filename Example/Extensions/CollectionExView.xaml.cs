// -----------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExView.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Example;

public partial class CollectionExView
{
    public CollectionExView()
    {
        InitializeComponent();

        DataContext = new CollectionExViewModel();
    }
}