// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocatorTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using Moq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class ServiceLocatorTests
{
    [Test]
    public void UseServiceLocator_CalledOnNull_ThrowsException()
    {
        IServiceProvider provider = null;

        Assert.That(() => provider.UseServiceLocator(), Throws.ArgumentNullException);
    }

    [Test]
    public void Register_CalledWithNull_ThrowsException()
    {
        IServiceProvider provider = null;

        Assert.That(() => ServiceLocator.Register(provider), Throws.ArgumentNullException);
    }

    [Test]
    public void Resolve_CalledAfterUseServiceLocator_ResolvesOnGivenProvider()
    {
        var provider = new Mock<IServiceProvider>();
        provider.Setup(x => x.GetService(typeof(string))).Returns("13");

        provider.Object.UseServiceLocator();
        var resolved = ServiceLocator.Resolve<string>();

        provider.Verify(x => x.GetService(typeof(string)), Times.Once);
        Assert.That(resolved, Is.EqualTo("13"));
    }

    [Test]
    public void Resolve_CalledAfterRegister_ResolvesOnGivenProvider()
    {
        var provider = new Mock<IServiceProvider>();
        provider.Setup(x => x.GetService(typeof(string))).Returns("13");

        ServiceLocator.Register(provider.Object);
        var resolved = ServiceLocator.Resolve<string>();

        provider.Verify(x => x.GetService(typeof(string)), Times.Once);
        Assert.That(resolved, Is.EqualTo("13"));
    }

    [Test]
    public void Resolve_CalledAfterUseServiceLocatorWithUnknownType_ReturnsNull()
    {
        var provider = new Mock<IServiceProvider>();
        provider.Setup(x => x.GetService(typeof(string))).Returns("13");

        provider.Object.UseServiceLocator();
        var resolved = ServiceLocator.Resolve<ServiceLocatorTests>();

        provider.Verify(x => x.GetService(typeof(ServiceLocatorTests)), Times.Once);
        Assert.That(resolved, Is.Null);
    }

    [Test]
    public void Resolve_CalledAfterRegisterWithUnknownType_ReturnsNull()
    {
        var provider = new Mock<IServiceProvider>();
        provider.Setup(x => x.GetService(typeof(string))).Returns("13");

        ServiceLocator.Register(provider.Object);
        var resolved = ServiceLocator.Resolve<ServiceLocatorTests>();

        provider.Verify(x => x.GetService(typeof(ServiceLocatorTests)), Times.Once);
        Assert.That(resolved, Is.Null);
    }
}