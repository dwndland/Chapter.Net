// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ListExViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class ListExViewModel
{
    private void IndexOf()
    {
        var collection = new List<int> { 1, 4, 3, 4, 1 };

        var result = collection.IndexOf(x => x > 3);
    }

    private void LastIndexOf()
    {
        var collection = new List<int> { 1, 2, 3 };

        var result = collection.LastIndexOf(x => x > 3);
    }

    private void RemoveFirst()
    {
        var collection = new List<int> { 1, 2, 3 };

        var result = collection.RemoveFirst(x => x > 3);
    }

    private void RemoveLast()
    {
        var collection = new List<int> { 1, 2, 3 };

        var result = collection.RemoveLast(x => x > 3);
    }

    private void AddRangeIf()
    {
        var collection = new List<string> { "1", "2", "3" };
        var items = new List<string> { "1", "1", "4", "5" };

        collection.AddRangeIf(items, x => x != "1");
    }

    private void AddRangeIfNotNull()
    {
        var collection = new List<string> { "1", "2", "3" };
        var items = new List<string> { null, null, "4", "5" };

        collection.AddRangeIfNotNull(items);
    }

    private void InsertIf()
    {
        var collection = new List<string> { "1", "2", "4" };

        collection.InsertIf(2, "1", x => x != "1");
        collection.InsertIf(2, "3", x => x != "1");
    }

    private void InsertIfNotNull()
    {
        var collection = new List<string> { "1", "2", "4" };

        collection.InsertIfNotNull(2, null);
        collection.InsertIfNotNull(2, "3");
    }

    private void InsertRangeIf()
    {
        var collection = new List<string> { "1", "2", "5" };
        var items = new List<string> { "1", "1", "3", "4" };

        collection.InsertRangeIf(2, items, x => x != "1");
    }

    private void InsertRangeIfNotNull()
    {
        var collection = new List<string> { "1", "2", "5" };
        var items = new List<string> { null, null, "3", "4" };

        collection.InsertRangeIfNotNull(2, items);
    }
}