// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TaskExtensionsTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class TaskExtensionsTests
{
    // Argument null exceptions cannot be tested, it crashes the runner in background.

    [Test]
    public async Task FireAndForget_Called_ExecutesTheTaskInBackground()
    {
        var source = new TaskCompletionSource();

        Execute().FireAndForget();

        await source.Task;

        return;

        Task Execute()
        {
            source.SetResult();
            return Task.CompletedTask;
        }
    }

    [Test]
    public async Task FireAndForget_CalledWithFollowUpAction_ExecutesBoth()
    {
        var source = new TaskCompletionSource();

        Execute().FireAndForget(Followup);

        await source.Task;

        return;

        Task Execute()
        {
            source.SetResult();
            return Task.CompletedTask;
        }

        void Followup()
        {
            Assert.That(source.Task.Status, Is.EqualTo(TaskStatus.RanToCompletion));
        }
    }

    [Test]
    public async Task FireAndForget_CalledWithFollowUpTask_ExecutesBoth()
    {
        var source = new TaskCompletionSource();

        Execute().FireAndForget(Followup);

        await source.Task;

        return;

        Task Execute()
        {
            source.SetResult();
            return Task.CompletedTask;
        }

        async Task Followup()
        {
            Assert.That(source.Task.Status, Is.EqualTo(TaskStatus.RanToCompletion));
            await Task.CompletedTask;
        }
    }

    [Test]
    public void FireAndForget_CalledWithExceptionCallbackAndTaskRaises_CallbackIsCalled()
    {
        var triggered = false;

        Execute().FireAndForget(OnError);

        Assert.That(triggered, Is.True);

        return;

        Task Execute()
        {
            return Task.FromException(new InvalidOperationException());
        }

        void OnError(Exception obj)
        {
            triggered = true;
        }
    }

    [Test]
    public void FireAndForget_CalledWithFollowUpActionAndExceptionCallbackAndTaskRaises_CallbackIsCalled()
    {
        var triggered = false;

        Execute().FireAndForget(Followup, OnError);

        Assert.That(triggered, Is.True);

        return;

        Task Execute()
        {
            return Task.FromException(new InvalidOperationException());
        }

        void Followup()
        {
            Assert.Fail();
        }

        void OnError(Exception obj)
        {
            triggered = true;
        }
    }

    [Test]
    public void FireAndForget_CalledWithFollowUpActionAndExceptionCallbackAndActionRaises_CallbackIsCalled()
    {
        var triggered = false;

        Execute().FireAndForget(Followup, OnError);

        Assert.That(triggered, Is.True);

        return;

        Task Execute()
        {
            return Task.CompletedTask;
        }

        void Followup()
        {
            throw new InvalidOperationException();
        }

        void OnError(Exception obj)
        {
            triggered = true;
        }
    }

    [Test]
    public void FireAndForget_CalledWithFollowUpTaskAndExceptionCallbackAndTaskRaises_CallbackIsCalled()
    {
        var triggered = false;

        Execute().FireAndForget(Followup, OnError);

        Assert.That(triggered, Is.True);

        return;

        Task Execute()
        {
            return Task.FromException(new InvalidOperationException());
        }

        Task Followup()
        {
            Assert.Fail();
            return Task.CompletedTask;
        }

        void OnError(Exception obj)
        {
            triggered = true;
        }
    }

    [Test]
    public void FireAndForget_CalledWithFollowUpTaskAndExceptionCallbackAndActionRaises_CallbackIsCalled()
    {
        var triggered = false;

        Execute().FireAndForget(Followup, OnError);

        Assert.That(triggered, Is.True);

        return;

        Task Execute()
        {
            return Task.CompletedTask;
        }

        Task Followup()
        {
            throw new InvalidOperationException();
        }

        void OnError(Exception obj)
        {
            triggered = true;
        }
    }

    [Test]
    public async Task FireAndForgetT_Called_ExecutesTheTaskInBackground()
    {
        var source = new TaskCompletionSource();

        Execute().FireAndForget();

        await source.Task;

        return;

        Task<int> Execute()
        {
            source.SetResult();
            return Task.FromResult(13);
        }
    }

    [Test]
    public async Task FireAndForgetT_CalledWithFollowUpAction_ExecutesBoth()
    {
        var source = new TaskCompletionSource();

        Execute().FireAndForget(Followup);

        await source.Task;

        return;

        Task<int> Execute()
        {
            source.SetResult();
            return Task.FromResult(13);
        }

        void Followup(int e)
        {
            Assert.Multiple(() =>
            {
                Assert.That(e, Is.EqualTo(13));
                Assert.That(source.Task.Status, Is.EqualTo(TaskStatus.RanToCompletion));
            });
        }
    }

    [Test]
    public async Task FireAndForgetT_CalledWithFollowUpTask_ExecutesBoth()
    {
        var source = new TaskCompletionSource();

        Execute().FireAndForget(Followup);

        await source.Task;

        return;

        Task<int> Execute()
        {
            source.SetResult();
            return Task.FromResult(13);
        }

        async Task Followup(int e)
        {
            Assert.Multiple(() =>
            {
                Assert.That(e, Is.EqualTo(13));
                Assert.That(source.Task.Status, Is.EqualTo(TaskStatus.RanToCompletion));
            });
            await Task.CompletedTask;
        }
    }

    [Test]
    public void FireAndForgetT_CalledWithFollowUpActionAndExceptionCallbackAndActionRaises_CallbackIsCalled()
    {
        var triggered = false;

        Execute().FireAndForget(Followup, OnError);

        Assert.That(triggered, Is.True);

        return;

        Task<int> Execute()
        {
            return Task.FromResult(13);
        }

        void Followup(int e)
        {
            Assert.That(e, Is.EqualTo(13));
            throw new InvalidOperationException();
        }

        void OnError(Exception obj)
        {
            triggered = true;
        }
    }

    [Test]
    public void FireAndForgetT_CalledWithFollowUpTaskAndExceptionCallbackAndActionRaises_CallbackIsCalled()
    {
        var triggered = false;

        Execute().FireAndForget(Followup, OnError);

        Assert.That(triggered, Is.True);

        return;

        Task<int> Execute()
        {
            return Task.FromResult(13);
        }

        Task Followup(int e)
        {
            Assert.That(e, Is.EqualTo(13));
            throw new InvalidOperationException();
        }

        void OnError(Exception obj)
        {
            triggered = true;
        }
    }
}