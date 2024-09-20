// -----------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExViewModel.cs" company="dwndland">
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

        ICollection<int> collection = new List<int>();
        foreach (var i in input)
            collection.AddIf(i, x => x > 15);
    }

    private void AddIfNotNull()
    {
        var input = new List<string> { "1", "2", null, "3", null };

        ICollection<string> target = new List<string>();
        foreach (var i in input)
            target.AddIfNotNull(i);
    }

    private void AddIfNeeded()
    {
        ICollection<string> list = new List<string> { "1", "2", "4" };

        list.AddIfNeeded("1");
        list.AddIfNeeded("5");
    }
}