// -----------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class EnumerableExTests
{
    [Test]
    public void Repeat_CalledWithNullCallback_ThrowsException()
    {
        Func<int> func = null;

        Assert.That(() => EnumerableEx.Repeat(func, 13).ToList(), Throws.ArgumentNullException);
    }

    [Test]
    public void Repeat_Called_CreatesTheCollectionRepeated()
    {
        var i = 1;
        var expected = new List<int> { 1, 2, 3, 4 };

        var list = EnumerableEx.Repeat(() => i++, 4).ToList();

        Assert.That(list, Is.EqualTo(expected));
    }

    [Test]
    public void ForEach_CalledOnNullCollection_ThrowsException()
    {
        IEnumerable<int> collection = null;

        Assert.That(() => collection.ForEach(_ => { }), Throws.ArgumentNullException);
    }

    [Test]
    public void ForEach_CalledWithNullCallback_ThrowsException()
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        IEnumerable<int> collection = new List<int>();

        Assert.That(() => collection.ForEach(null), Throws.ArgumentNullException);
    }

    [Test]
    public void ForEach_Called_CallsCallbackForEachItem()
    {
        var source = new List<int> { 44, 12, 3 };
        var target = new List<int>();

        source.ForEach(x => target.Add(x));

        Assert.That(source, Is.EqualTo(target));
    }

    [Test]
    public void Shuffle_CalledOnNullCollection_ThrowsException()
    {
        List<int> collection = null;

        Assert.That(() => collection.Shuffle().ToList(), Throws.ArgumentNullException);
    }

    [Test]
    public void Shuffle_Called_CreatesNewShuffledCollection()
    {
        var source = new List<int> { 44, 12, 3, 15, 50, 456 };

        var target1 = source.Shuffle();
        var target2 = source.Shuffle();

        Assert.That(source, Is.Not.EqualTo(target1));
        Assert.That(source, Is.EquivalentTo(target1));
        Assert.That(source, Is.Not.EqualTo(target2));
        Assert.That(source, Is.EquivalentTo(target2));
    }
}