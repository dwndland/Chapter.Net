// -----------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExTests.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class CollectionExTests
{
    [Test]
    public void AddIf_Called_AddOnlyThoseWhereTheConditionMatches()
    {
        var input = new List<int> { 1, 15, 22, 16 };
        var expected = new List<int> { 22, 16 };

        var target = new List<int>();
        foreach (var i in input)
            target.AddIf(i, x => x > 15);

        Assert.That(target, Is.EqualTo(expected));
    }

    [Test]
    public void AddIf_CalledOnNullList_ThrowsException()
    {
        List<int> target = null;

        Assert.That(() => target.AddIf(1, _ => true), Throws.ArgumentNullException);
    }

    [Test]
    public void AddIf_CalledWithNullCondition_ThrowsException()
    {
        var target = new List<int>();

        Assert.That(() => target.AddIf(1, null), Throws.ArgumentNullException);
    }

    [Test]
    public void AddIfNotNull_CalledOnNullList_ThrowsException()
    {
        List<string> target = null;

        Assert.That(() => target.AddIfNotNull(""), Throws.ArgumentNullException);
    }

    [Test]
    public void AddIfNotNull_Called_AddOnlyThoseWhereTheConditionMatches()
    {
        var input = new List<string> { "1", "2", null, "3", null };
        var expected = new List<string> { "1", "2", "3" };

        var target = new List<string>();
        foreach (var i in input)
            target.AddIfNotNull(i);

        Assert.That(target, Is.EqualTo(expected));
    }
}