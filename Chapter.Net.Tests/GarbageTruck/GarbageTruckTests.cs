// -----------------------------------------------------------------------------------------------------------------
// <copyright file="GarbageTruckTests.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class GarbageTruckTests
{
    private GarbageTruck _target;

    [SetUp]
    public void SetUp()
    {
        _target = new GarbageTruck();
    }

    [Test]
    public void Add_CalledWithNull_ThrowsException()
    {
        Assert.That(() => _target.Add(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Dispose_Called_DisposesAllAddedBefore()
    {
        var first = new TestableGarbageTruckClass();
        var second = new TestableGarbageTruckClass();
        var third = new TestableGarbageTruckClass();
        _target.Add(first);
        _target.Add(second);
        _target.Add(third);

        _target.Dispose();

        Assert.Multiple(() =>
        {
            Assert.That(first.IsDisposed, Is.True);
            Assert.That(second.IsDisposed, Is.True);
            Assert.That(third.IsDisposed, Is.True);
        });
    }

    [Test]
    public void Dispose_CalledTwice_DisposesOnlyOnce()
    {
        var first = new TestableGarbageTruckClass();
        var second = new TestableGarbageTruckClass();
        var third = new TestableGarbageTruckClass();
        _target.Add(first);
        _target.Add(second);
        _target.Add(third);

        _target.Dispose();
        first.IsDisposed = false;
        second.IsDisposed = false;
        third.IsDisposed = false;
        _target.Dispose();

        Assert.Multiple(() =>
        {
            Assert.That(first.IsDisposed, Is.False);
            Assert.That(second.IsDisposed, Is.False);
            Assert.That(third.IsDisposed, Is.False);
        });
    }
}