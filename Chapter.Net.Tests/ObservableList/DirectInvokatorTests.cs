// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DirectInvokatorTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class DirectInvokatorTests
{
    private DirectInvokator _target;

    [SetUp]
    public void SetUp()
    {
        _target = new DirectInvokator();
    }

    [Test]
    public void Invoke_CalledWithNull_ThrowsException()
    {
        Action action = null;

        Assert.That(() => _target.Invoke(action), Throws.ArgumentNullException);
    }

    [Test]
    public void Invoke_CalledWithAction_CallsTheAction()
    {
        var triggered = false;

        _target.Invoke(Callback);

        Assert.That(triggered, Is.True);
        return;

        void Callback()
        {
            triggered = true;
        }
    }
}