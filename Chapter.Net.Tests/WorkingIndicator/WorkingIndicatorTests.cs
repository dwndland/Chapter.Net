// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WorkingIndicatorTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class WorkingIndicatorTests
{
    [Test]
    public void IsActive_CalledWithNull_ReturnsFalse()
    {
        var isActive = WorkingIndicator.IsActive(null);

        Assert.That(isActive, Is.False);
    }

    [Test]
    public void IsActive_CalledWithNonDisposedInstance_ReturnsTrue()
    {
        var indicator = new WorkingIndicator();

        var isActive = WorkingIndicator.IsActive(indicator);

        Assert.That(isActive, Is.True);
    }

    [Test]
    public void IsActive_CalledWithNonDisposedInstance_ReturnsFalse()
    {
        var indicator = new WorkingIndicator();

        indicator.Dispose();
        var isActive = WorkingIndicator.IsActive(indicator);

        Assert.That(isActive, Is.False);
    }
}