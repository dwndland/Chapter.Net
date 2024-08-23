// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncDelegateCommandTests.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class AsyncDelegateCommandTests
{
    [Test]
    public void Ctor_CalledWithNullCanExecute1_ThrowsException()
    {
        Assert.That(() => new AsyncDelegateCommand(null, () => Task.CompletedTask), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullCanExecute2_ThrowsException()
    {
        Assert.That(() => new AsyncDelegateCommand<int>(null, _ => Task.CompletedTask), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullExecute1_ThrowsException()
    {
        Assert.That(() => new AsyncDelegateCommand(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullExecute2_ThrowsException()
    {
        Assert.That(() => new AsyncDelegateCommand(() => true, null), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullExecute3_ThrowsException()
    {
        Assert.That(() => new AsyncDelegateCommand<int>(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_CalledWithNullExecute4_ThrowsException()
    {
        Assert.That(() => new AsyncDelegateCommand<int>(_ => true, null), Throws.ArgumentNullException);
    }

    [Test]
    public void CanExecute_NoCallback_ReturnsTrue()
    {
        var target = new AsyncDelegateCommand(() => Task.CompletedTask);

        var canExecute = target.CanExecute();

        Assert.That(canExecute, Is.True);
    }

    [Test]
    public void CanExecute_CallbackSaysTrue_ReturnsTrue()
    {
        var target = new AsyncDelegateCommand(() => true, () => Task.CompletedTask);

        var canExecute = target.CanExecute();

        Assert.That(canExecute, Is.True);
    }

    [Test]
    public void CanExecute_CallbackSaysFalse_ReturnsFalse()
    {
        var target = new AsyncDelegateCommand(() => false, () => Task.CompletedTask);

        var canExecute = target.CanExecute();

        Assert.That(canExecute, Is.False);
    }

    [Test]
    public void Ctor_CreatedWithParameter_CanExecuteForwardsThem()
    {
        var target = new AsyncDelegateCommand<int>(
            c =>
            {
                Assert.That(c, Is.EqualTo(13));
                return true;
            },
            _ => Task.CompletedTask);

        target.CanExecute(13);
    }

    [Test]
    public void Ctor_CreatedWithParameter_ExecuteForwardsThem()
    {
        var target = new AsyncDelegateCommand<int>(
            _ => true,
            e =>
            {
                Assert.That(e, Is.EqualTo(13));
                return Task.CompletedTask;
            });

        target.CanExecute(13);
    }

    [Test]
    public void Execute_Called_ExecutesTheCallback()
    {
        var triggered = false;
        var target = new AsyncDelegateCommand(() =>
        {
            triggered = true;
            return Task.CompletedTask;
        });

        target.Execute();

        Assert.That(triggered, Is.True);
    }

    [Test]
    public void Execute_CalledWithWrongParameter_CanExecuteRaisesInvalidCastException()
    {
        var target = new AsyncDelegateCommand<int>(_ => true, _ => Task.CompletedTask);

        var act = () => target.CanExecute("Demo");

        Assert.That(act, Throws.TypeOf<InvalidCastException>());
    }

    [Test]
    public async Task Execute_CalledWithWrongParameter_ExecuteRaisesInvalidCastException()
    {
        var target = new AsyncDelegateCommand<int>(_ => true, _ => Task.CompletedTask);
        var triggered = false;

        try
        {
            await target.ExecuteAsync("Demo");
        }
        catch (InvalidCastException)
        {
            triggered = true;
        }
        catch (Exception)
        {
            Assert.Fail();
        }

        Assert.That(triggered, Is.True);
    }

    [Test]
    public async Task Execute_NeedsSomeTime_CommandStaysDisabled()
    {
        AsyncDelegateCommand target = null;

        target = new AsyncDelegateCommand(
            () => true,
            async () =>
            {
                await Task.CompletedTask;
                // ReSharper disable once AccessToModifiedClosure
                Assert.That(target.CanExecute(null), Is.False);
                await Task.CompletedTask;
            });

        Assert.That(target.CanExecute(), Is.True);

        await target.ExecuteAsync();

        Assert.That(target.CanExecute(), Is.True);
    }
}