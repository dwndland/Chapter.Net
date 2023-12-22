// -----------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class EnumerableExViewModel
{
    private void Repeat()
    {
        var i = 1;
        var list = EnumerableEx.Repeat(() => i++, 4);
    }

    private void ForEach()
    {
        IEnumerable<int> source = new List<int> { 44, 12, 3 };
        var target = new List<int>();

        source.ForEach(x => target.Add(x));
    }

    private void Shuffle()
    {
        var source = new List<int> { 44, 12, 3, 15, 50, 456 };

        var shuffled = source.Shuffle();
    }
}