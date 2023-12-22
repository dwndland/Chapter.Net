// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NameOfViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example.NameOfNamespace;

public class NameOfViewModel
{
    public string Name => NameOf.Name<NameOfViewModel>();

    public string NameWithProperty => NameOf.Name<NameOfViewModel>(nameof(Name));

    public string Namespace => NameOf.Namespace<NameOfViewModel>();

    public string FullName => NameOf.FullName<NameOfViewModel>();

    public string FullNameWithProperty => NameOf.FullName<NameOfViewModel>(nameof(Name));
}