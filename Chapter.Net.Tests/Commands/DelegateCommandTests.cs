// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateCommandTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class DelegateCommandTests
{
    [Test]
    public void Ctor_CalledWithNullCanExecute1_ThrowsException()
    {
        Assert.That(() => new DelegateCommand(null, () => { }), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullCanExecute2_ThrowsException()
    {
        Assert.That(() => new DelegateCommand<int>(null, _ => { }), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullExecute1_ThrowsException()
    {
        Assert.That(() => new DelegateCommand(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullExecute2_ThrowsException()
    {
        Assert.That(() => new DelegateCommand(() => true, null), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullExecute3_ThrowsException()
    {
        Assert.That(() => new DelegateCommand<int>(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullExecute4_ThrowsException()
    {
        Assert.That(() => new DelegateCommand<int>(_ => true, null), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CreatedWithParameter_CanExecuteForwardsThem()
    {
        var target = new DelegateCommand<int>(
            c =>
            {
                Assert.That(c, Is.EqualTo(13));
                return true;
            },
            _ => { });

        target.CanExecute(13);
    }

    [Test]
    public void Ctor_CreatedWithParameter_ExecuteForwardsThem()
    {
        var target = new DelegateCommand<int>(
            _ => true,
            e => { Assert.That(e, Is.EqualTo(13)); });

        target.CanExecute(13);
    }

    [Test]
    public void CanExecute_NoCallback_ReturnsTrue()
    {
        var target = new DelegateCommand(() => { });

        var canExecute = target.CanExecute(null);

        Assert.That(canExecute, Is.True);
    }

    [Test]
    public void CanExecute_CallbackSaysTrue_ReturnsTrue()
    {
        var target = new DelegateCommand(() => true, () => { });

        var canExecute = target.CanExecute(null);

        Assert.That(canExecute, Is.True);
    }

    [Test]
    public void CanExecute_CallbackSaysFalse_ReturnsFalse()
    {
        var target = new DelegateCommand(() => false, () => { });

        var canExecute = target.CanExecute(null);

        Assert.That(canExecute, Is.False);
    }

    [Test]
    public void CanExecute_CreatedWithoutParameterButOneIsGiven_IgnoresTheParameter()
    {
        var target = new DelegateCommand(() => { });

        target.CanExecute("Demo");
    }

    [Test]
    public void Execute_Called_ExecutesTheCallback()
    {
        var triggered = false;
        var target = new DelegateCommand(() => triggered = true);

        target.Execute(null);

        Assert.That(triggered, Is.True);
    }

    [Test]
    public void Execute_CalledWithWrongParameter_CanExecuteRaisesInvalidCastException()
    {
        var target = new DelegateCommand<int>(_ => true, _ => { });

        var act = () => target.CanExecute("Demo");

        Assert.That(act, Throws.TypeOf<InvalidCastException>());
    }

    [Test]
    public void Execute_CalledWithWrongParameter_ExecuteRaisesInvalidCastException()
    {
        var target = new DelegateCommand<int>(_ => true, _ => { });

        var act = () => target.Execute("Demo");

        Assert.That(act, Throws.TypeOf<InvalidCastException>());
    }

    [Test]
    public void Execute_CreatedWithoutParameterButOneIsGiven_IgnoresTheParameter()
    {
        var target = new DelegateCommand(() => { });

        target.Execute("Demo");
    }
}