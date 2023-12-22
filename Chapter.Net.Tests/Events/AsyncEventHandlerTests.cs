// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncEventHandlerTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class AsyncEventHandlerTests
{
    private TestableAsyncEventHandler _target;

    [SetUp]
    public void SetUp()
    {
        _target = new TestableAsyncEventHandler();
    }

    [Test]
    public void Invoke_WithoutEventArgsCalledWithoutSubscribers_RaisesNullReferenceException()
    {
        Assert.That(async () => await _target.TestEventWithoutEventArgs.Invoke(null, EventArgs.Empty), Throws.TypeOf<NullReferenceException>());
    }

    [Test]
    public async Task Invoke_WithoutEventArgsCalledWithSubscribers_AllSubscribersGetCalledAsFireAndForget()
    {
        var triggeredOne = false;
        var triggeredTwo = false;

        _target.TestEventWithoutEventArgs += EventOne;
        _target.TestEventWithoutEventArgs += EventTwo;

        await _target.TestEventWithoutEventArgs.Invoke(_target, EventArgs.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(triggeredOne, Is.False);
            Assert.That(triggeredTwo, Is.True);
        });
        return;

        async Task EventOne(object sender, EventArgs e)
        {
            await Task.Delay(100);
            triggeredOne = true;
        }

        Task EventTwo(object sender, EventArgs e)
        {
            triggeredTwo = true;
            return Task.CompletedTask;
        }
    }

    [Test]
    public void Invoke_WithEventArgsCalledWithoutSubscribers_RaisesNullReferenceException()
    {
        Assert.That(async () => await _target.TestEventWithEventArgs.Invoke(null, new MyEventArgs()), Throws.TypeOf<NullReferenceException>());
    }

    [Test]
    public async Task Invoke_WithEventArgsCalledWithSubscribers_AllSubscribersGetCalledAsFireAndForget()
    {
        var triggeredOne = false;
        var triggeredTwo = false;

        _target.TestEventWithEventArgs += EventOne;
        _target.TestEventWithEventArgs += EventTwo;

        await _target.TestEventWithEventArgs.Invoke(_target, new MyEventArgs());

        Assert.Multiple(() =>
        {
            Assert.That(triggeredOne, Is.False);
            Assert.That(triggeredTwo, Is.True);
        });
        return;

        async Task EventOne(object sender, MyEventArgs e)
        {
            await Task.Delay(100);
            triggeredOne = true;
        }

        Task EventTwo(object sender, MyEventArgs e)
        {
            triggeredTwo = true;
            return Task.CompletedTask;
        }
    }

    [Test]
    public void InvokeAll_WithoutEventArgsCalledWithoutSubscribers_RaisesNullReferenceException()
    {
        Assert.That(async () => await _target.TestEventWithoutEventArgs.InvokeAll(null, EventArgs.Empty), Throws.TypeOf<NullReferenceException>());
    }

    [Test]
    public async Task InvokeAll_WithoutEventArgsCalledWithSubscribers_AllSubscribersGetCalledInAnOrder()
    {
        var triggeredOne = false;
        var triggeredTwo = false;

        _target.TestEventWithoutEventArgs += EventOne;
        _target.TestEventWithoutEventArgs += EventTwo;

        await _target.TestEventWithoutEventArgs.InvokeAll(_target, EventArgs.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(triggeredOne, Is.True);
            Assert.That(triggeredTwo, Is.True);
        });
        return;

        async Task EventOne(object sender, EventArgs e)
        {
            await Task.Delay(100);
            triggeredOne = true;
        }

        Task EventTwo(object sender, EventArgs e)
        {
            triggeredTwo = true;
            return Task.CompletedTask;
        }
    }

    [Test]
    public void InvokeAll_WithEventArgsCalledWithoutSubscribers_RaisesNullReferenceException()
    {
        Assert.That(async () => await _target.TestEventWithEventArgs.InvokeAll(null, new MyEventArgs()), Throws.TypeOf<NullReferenceException>());
    }

    [Test]
    public async Task InvokeAll_WithEventArgsCalledWithSubscribers_AllSubscribersGetCalled()
    {
        var triggeredOne = false;
        var triggeredTwo = false;

        _target.TestEventWithEventArgs += EventOne;
        _target.TestEventWithEventArgs += EventTwo;

        await _target.TestEventWithEventArgs.InvokeAll(_target, new MyEventArgs());

        Assert.Multiple(() =>
        {
            Assert.That(triggeredOne, Is.True);
            Assert.That(triggeredTwo, Is.True);
        });
        return;

        async Task EventOne(object sender, MyEventArgs e)
        {
            await Task.Delay(100);
            triggeredOne = true;
        }

        Task EventTwo(object sender, MyEventArgs e)
        {
            triggeredTwo = true;
            return Task.CompletedTask;
        }
    }

    [Test]
    public void GetEventHandlers_WithoutEventArgsCalledWithoutSubscribers_RaisesNullReferenceException()
    {
        Assert.That(() => _target.TestEventWithoutEventArgs.GetEventHandlers(), Throws.TypeOf<NullReferenceException>());
    }

    [Test]
    public void GetEventHandlers_WithoutEventArgsCalledWithSubscribers_ReturnsListOfSubscribers()
    {
        _target.TestEventWithoutEventArgs += EventOne;
        _target.TestEventWithoutEventArgs += EventTwo;

        var handlers = _target.TestEventWithoutEventArgs.GetEventHandlers().ToList();

        Assert.That(handlers, Has.Count.EqualTo(2));
        return;

        Task EventOne(object sender, EventArgs e)
        {
            _target.TestEventWithoutEventArgs -= EventOne;
            return Task.CompletedTask;
        }

        Task EventTwo(object sender, EventArgs e)
        {
            _target.TestEventWithoutEventArgs -= EventTwo;
            return Task.CompletedTask;
        }
    }

    [Test]
    public void GetEventHandlers_WithEventArgsCalledWithoutSubscribers_RaisesNullReferenceException()
    {
        Assert.That(() => _target.TestEventWithEventArgs.GetEventHandlers(), Throws.TypeOf<NullReferenceException>());
    }

    [Test]
    public void GetEventHandlers_WithEventArgsCalledWithSubscribers_ReturnsListOfSubscribers()
    {
        _target.TestEventWithEventArgs += EventOne;
        _target.TestEventWithEventArgs += EventTwo;

        var handlers = _target.TestEventWithEventArgs.GetEventHandlers().ToList();

        Assert.That(handlers, Has.Count.EqualTo(2));
        return;

        Task EventOne(object sender, MyEventArgs e)
        {
            _target.TestEventWithEventArgs -= EventOne;
            return Task.CompletedTask;
        }

        Task EventTwo(object sender, MyEventArgs e)
        {
            _target.TestEventWithEventArgs -= EventTwo;
            return Task.CompletedTask;
        }
    }
}