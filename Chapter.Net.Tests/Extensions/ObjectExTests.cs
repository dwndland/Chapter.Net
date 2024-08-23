// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExTests.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class ObjectExTests
{
    [Test]
    public void IsNullOrEmpty_CalledOnNullObject_ReturnsTrue()
    {
        object item = null;

        var result = item.IsNullOrEmpty();

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsNullOrEmpty_CalledOnNotNullFilledObject_ReturnsFalse1()
    {
        var result = 1.IsNullOrEmpty();

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsNullOrEmpty_CalledOnNotNullFilledObject_ReturnsFalse2()
    {
        var result = "Demo".IsNullOrEmpty();

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsNullOrEmpty_CalledOnEmptyObject_ReturnsTrue()
    {
        var item = string.Empty;

        var result = item.IsNullOrEmpty();

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsNullOrWhiteSpace_CalledOnNullObject_ReturnsTrue()
    {
        object item = null;

        var result = item.IsNullOrWhiteSpace();

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsNullOrWhiteSpace_CalledOnNotNullFilledObject_ReturnsFalse1()
    {
        var result = 1.IsNullOrWhiteSpace();

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsNullOrWhiteSpace_CalledOnNotNullFilledObject_ReturnsFalse2()
    {
        var result = "Demo".IsNullOrWhiteSpace();

        Assert.That(result, Is.False);
    }

    [Test]
    public void IsNullOrWhiteSpace_CalledOnEmptyObject_ReturnsTrue()
    {
        var item = string.Empty;

        var result = item.IsNullOrWhiteSpace();

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsNullOrWhiteSpace_CalledOnWhitespaceObject_ReturnsTrue()
    {
        var result = "   ".IsNullOrWhiteSpace();

        Assert.That(result, Is.True);
    }
}