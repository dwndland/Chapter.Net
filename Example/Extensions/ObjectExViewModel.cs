// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class ObjectExViewModel
{
    private void IsNullOrEmpty()
    {
        var result = "Demo".IsNullOrEmpty();
    }

    private void IsNullOrWhiteSpace()
    {
        var result = "Demo".IsNullOrWhiteSpace();
    }
}