// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ListExTests.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class ListExTests
{
    [Test]
    public void IndexOf_CalledOnNullCollection_ThrowsException()
    {
        List<int> collection = null;

        Assert.That(() => collection.IndexOf(_ => false), Throws.ArgumentNullException);
    }

    [Test]
    public void IndexOf_CalledWithNullCondition_ThrowsException()
    {
        var collection = new List<int>();
        Func<int, bool> condition = null;

        Assert.That(() => collection.IndexOf(condition), Throws.ArgumentNullException);
    }

    [Test]
    public void IndexOf_NothingMatches_ReturnsMinusOne()
    {
        var collection = new List<int> { 1, 2, 3 };

        var result = collection.IndexOf(x => x > 3);

        Assert.That(result, Is.EqualTo(-1));
    }

    [Test]
    public void IndexOf_MultipleMatches_ReturnsFirstMatch()
    {
        var collection = new List<int> { 1, 4, 3, 4, 1 };

        var result = collection.IndexOf(x => x > 3);

        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void LastIndexOf_CalledOnNullCollection_ThrowsException()
    {
        List<int> collection = null;

        Assert.That(() => collection.LastIndexOf(_ => false), Throws.ArgumentNullException);
    }

    [Test]
    public void LastIndexOf_CalledWithNullCondition_ThrowsException()
    {
        var collection = new List<int>();
        Func<int, bool> condition = null;

        Assert.That(() => collection.LastIndexOf(condition), Throws.ArgumentNullException);
    }

    [Test]
    public void LastIndexOf_NothingMatches_ReturnsMinusOne()
    {
        var collection = new List<int> { 1, 2, 3 };

        var result = collection.LastIndexOf(x => x > 3);

        Assert.That(result, Is.EqualTo(-1));
    }

    [Test]
    public void LastIndexOf_MultipleMatches_ReturnsLastMatch()
    {
        var collection = new List<int> { 1, 4, 3, 4, 1 };

        var result = collection.LastIndexOf(x => x > 3);

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void RemoveFirst_CalledOnNullCollection_ThrowsException()
    {
        List<int> collection = null;

        Assert.That(() => collection.RemoveFirst(_ => false), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveFirst_CalledWithNullFilter_ThrowsException()
    {
        var collection = new List<int>();
        Predicate<int> condition = null;

        Assert.That(() => collection.RemoveFirst(condition), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveFirst_NothingMatches_RemovesNothing()
    {
        var collection = new List<int> { 1, 2, 3 };
        var expected = new List<int> { 1, 2, 3 };

        var result = collection.RemoveFirst(x => x > 3);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(collection, Is.EqualTo(expected));
        });
    }

    [Test]
    public void RemoveFirst_MultipleMatches_RemovesFirstMatch()
    {
        var collection = new List<int> { 1, 4, 3, 4, 1 };
        var expected = new List<int> { 1, 3, 4, 1 };

        var result = collection.RemoveFirst(x => x > 3);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(collection, Is.EqualTo(expected));
        });
    }

    [Test]
    public void RemoveLast_CalledOnNullCollection_ThrowsException()
    {
        List<int> collection = null;

        Assert.That(() => collection.RemoveLast(_ => false), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveLast_CalledWithNullFilter_ThrowsException()
    {
        var collection = new List<int>();
        Predicate<int> condition = null;

        Assert.That(() => collection.RemoveLast(condition), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveLast_NothingMatches_RemovesNothing()
    {
        var collection = new List<int> { 1, 2, 3 };
        var expected = new List<int> { 1, 2, 3 };

        var result = collection.RemoveLast(x => x > 3);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(collection, Is.EqualTo(expected));
        });
    }

    [Test]
    public void RemoveLast_MultipleMatches_RemovesLastMatch()
    {
        var collection = new List<int> { 1, 4, 3, 4, 1 };
        var expected = new List<int> { 1, 4, 3, 1 };

        var result = collection.RemoveLast(x => x > 3);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(collection, Is.EqualTo(expected));
        });
    }

    [Test]
    public void AddRangeIf_CalledOnNullCollection_ThrowsException()
    {
        List<string> collection = null;
        var items = new List<string> { "1", "2", "3" };

        Assert.That(() => collection.AddRangeIf(items, _ => true), Throws.ArgumentNullException);
    }

    [Test]
    public void AddRangeIf_CalledWithNullItems_ThrowsException()
    {
        var collection = new List<string>();

        Assert.That(() => collection.AddRangeIf(null, _ => true), Throws.ArgumentNullException);
    }

    [Test]
    public void AddRangeIf_CalledWithNullFilter_ThrowsException()
    {
        var collection = new List<string>();
        var items = new List<string> { "1", "2", "3" };

        Assert.That(() => collection.AddRangeIf(items, null), Throws.ArgumentNullException);
    }

    [Test]
    public void AddRangeIf_Called_AddsAllItemsWhichMatches()
    {
        var collection = new List<string> { "1", "2", "3" };
        var items = new List<string> { "1", "1", "4", "5" };
        var expected = new List<string> { "1", "2", "3", "4", "5" };

        collection.AddRangeIf(items, x => x != "1");

        Assert.That(collection, Is.EqualTo(expected));
    }

    [Test]
    public void AddRangeIfNotNull_CalledOnNullCollection_ThrowsException()
    {
        List<string> collection = null;
        var items = new List<string> { "1", "2", null, "3" };

        Assert.That(() => collection.AddRangeIfNotNull(items), Throws.ArgumentNullException);
    }

    [Test]
    public void AddRangeIfNotNull_CalledWithNullItems_ThrowsException()
    {
        var collection = new List<string>();

        Assert.That(() => collection.AddRangeIfNotNull(null), Throws.ArgumentNullException);
    }

    [Test]
    public void AddRangeIfNotNull_Called_AddsAllItemsWhichAreNotNull()
    {
        var collection = new List<string> { "1", "2", "3" };
        var items = new List<string> { null, null, "4", "5" };
        var expected = new List<string> { "1", "2", "3", "4", "5" };

        collection.AddRangeIfNotNull(items);

        Assert.That(collection, Is.EqualTo(expected));
    }

    [Test]
    public void InsertIf_CalledOnNullCollection_ThrowsException()
    {
        List<string> collection = null;

        Assert.That(() => collection.InsertIf(0, "13", _ => true), Throws.ArgumentNullException);
    }

    [Test]
    public void InsertIf_CalledWithNullFilter_ThrowsException()
    {
        var collection = new List<string>();

        Assert.That(() => collection.InsertIf(0, "13", null), Throws.ArgumentNullException);
    }

    [Test]
    public void InsertIf_CalledWthNegativeIndex_ThrowsException()
    {
        var collection = new List<string>();

        Assert.That(() => collection.InsertIf(-8, "13", _ => true), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void InsertIf_CalledWthTooHighIndex_ThrowsException()
    {
        var collection = new List<string>();

        Assert.That(() => collection.InsertIf(8, "13", _ => true), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void InsertIf_Called_InsertsTheItemsOnTheGivenSpot()
    {
        var collection = new List<string> { "1", "2", "4" };
        var expected = new List<string> { "1", "2", "3", "4" };

        collection.InsertIf(2, "1", x => x != "1");
        collection.InsertIf(2, "3", x => x != "1");

        Assert.That(collection, Is.EqualTo(expected));
    }

    [Test]
    public void InsertIfNotNull_CalledOnNullCollection_ThrowsException()
    {
        List<string> collection = null;

        Assert.That(() => collection.InsertIfNotNull(0, "13"), Throws.ArgumentNullException);
    }

    [Test]
    public void InsertIfNotNull_CalledWthNegativeIndex_ThrowsException()
    {
        var collection = new List<string>();

        Assert.That(() => collection.InsertIfNotNull(-8, "13"), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void InsertIfNotNull_CalledWthTooHighIndex_ThrowsException()
    {
        var collection = new List<string>();

        Assert.That(() => collection.InsertIfNotNull(8, "13"), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void InsertIfNotNull_Called_InsertsTheItemsOnTheGivenSpot()
    {
        var collection = new List<string> { "1", "2", "4" };
        var expected = new List<string> { "1", "2", "3", "4" };

        collection.InsertIfNotNull(2, null);
        collection.InsertIfNotNull(2, "3");

        Assert.That(collection, Is.EqualTo(expected));
    }

    [Test]
    public void InsertRangeIf_CalledOnNullCollection_ThrowsException()
    {
        List<string> collection = null;
        var items = new List<string> { "1", "2", null, "3" };

        Assert.That(() => collection.InsertRangeIf(0, items, _ => true), Throws.ArgumentNullException);
    }

    [Test]
    public void InsertRangeIf_CalledWithNullItems_ThrowsException()
    {
        var collection = new List<string>();

        Assert.That(() => collection.InsertRangeIf(0, null, _ => true), Throws.ArgumentNullException);
    }

    [Test]
    public void InsertRangeIf_CalledWithNullFilter_ThrowsException()
    {
        var collection = new List<string>();
        var items = new List<string> { "1", "2", null, "3" };

        Assert.That(() => collection.InsertRangeIf(0, items, null), Throws.ArgumentNullException);
    }

    [Test]
    public void InsertRangeIf_CalledWthNegativeIndex_ThrowsException()
    {
        var collection = new List<string>();
        var items = new List<string> { "1", "2", null, "3" };

        Assert.That(() => collection.InsertRangeIf(-8, items, _ => true), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void InsertRangeIf_CalledWthTooHighIndex_ThrowsException()
    {
        var collection = new List<string>();
        var items = new List<string> { "1", "2", null, "3" };

        Assert.That(() => collection.InsertRangeIf(8, items, _ => true), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void InsertRangeIf_Called_InsertsTheItemsOnTheGivenSpot()
    {
        var collection = new List<string> { "1", "2", "5" };
        var items = new List<string> { "1", "1", "3", "4" };
        var expected = new List<string> { "1", "2", "3", "4", "5" };

        collection.InsertRangeIf(2, items, x => x != "1");

        Assert.That(collection, Is.EqualTo(expected));
    }

    [Test]
    public void InsertRangeIfNotNull_CalledOnNullCollection_ThrowsException()
    {
        List<string> collection = null;
        var items = new List<string> { "1", "2", null, "3" };

        Assert.That(() => collection.InsertRangeIfNotNull(0, items), Throws.ArgumentNullException);
    }

    [Test]
    public void InsertRangeIfNotNull_CalledWithNullItems_ThrowsException()
    {
        List<string> collection = null;

        Assert.That(() => collection.InsertRangeIfNotNull(0, null), Throws.ArgumentNullException);
    }

    [Test]
    public void InsertRangeIfNotNull_CalledWthNegativeIndex_ThrowsException()
    {
        var collection = new List<string>();
        var items = new List<string> { "1", "2", null, "3" };

        Assert.That(() => collection.InsertRangeIfNotNull(-8, items), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void InsertRangeIfNotNull_CalledWthTooHighIndex_ThrowsException()
    {
        var collection = new List<string>();
        var items = new List<string> { "1", "2", null, "3" };

        Assert.That(() => collection.InsertRangeIfNotNull(8, items), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void InsertRangeIfNotNull_Called_InsertsTheItemsOnTheGivenSpot()
    {
        var collection = new List<string> { "1", "2", "5" };
        var items = new List<string> { null, null, "3", "4" };
        var expected = new List<string> { "1", "2", "3", "4", "5" };

        collection.InsertRangeIfNotNull(2, items);

        Assert.That(collection, Is.EqualTo(expected));
    }
}