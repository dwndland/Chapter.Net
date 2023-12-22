// -----------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class CollectionExViewModel
{
    private void AddIf()
    {
        var input = new List<int> { 1, 15, 22, 16 };

        var target = new List<int>();
        foreach (var i in input)
            target.AddIf(i, x => x > 15);
    }

    private void AddIfNotNull()
    {
        var input = new List<string> { "1", "2", null, "3", null };

        var target = new List<string>();
        foreach (var i in input)
            target.AddIfNotNull(i);
    }
}