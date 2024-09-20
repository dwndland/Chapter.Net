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
        var list = new List<int> { 1, 4, 3, 4, 1 };

        var result = list.IndexOf(x => x > 3);
    }

    private void LastIndexOf()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.LastIndexOf(x => x > 3);
    }

    private void RemoveFirst()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.RemoveFirst(x => x > 3);
    }

    private void RemoveLast()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.RemoveLast(x => x > 3);
    }

    private void AddRangeIf()
    {
        var list = new List<string> { "1", "2", "3" };
        var items = new List<string> { "1", "1", "4", "5" };

        list.AddRangeIf(items, x => x != "1");
    }

    private void AddRangeIfNotNull()
    {
        var list = new List<string> { "1", "2", "3" };
        var items = new List<string> { null, null, "4", "5" };

        list.AddRangeIfNotNull(items);
    }

    private void InsertIf()
    {
        var list = new List<string> { "1", "2", "4" };

        list.InsertIf(2, "1", x => x != "1");
        list.InsertIf(2, "3", x => x != "1");
    }

    private void InsertIfNotNull()
    {
        var list = new List<string> { "1", "2", "4" };

        list.InsertIfNotNull(2, null);
        list.InsertIfNotNull(2, "3");
    }

    private void InsertRangeIf()
    {
        var list = new List<string> { "1", "2", "5" };
        var items = new List<string> { "1", "1", "3", "4" };

        list.InsertRangeIf(2, items, x => x != "1");
    }

    private void InsertRangeIfNotNull()
    {
        var list = new List<string> { "1", "2", "5" };
        var items = new List<string> { null, null, "3", "4" };

        list.InsertRangeIfNotNull(2, items);
    }

    private void AddIfNeeded()
    {
        var list = new List<string> { "1", "2", "4" };

        list.AddIfNeeded("1");
        list.AddIfNeeded("5");
    }
}