// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TaskExtensions.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Extends a task with useful methods.
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    ///     Executes a task.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <param name="task">The task to execute.</param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    public static async void FireAndForget(this Task task)
    {
        ArgumentNullException.ThrowIfNull(task);

        await task;
    }

    /// <summary>
    ///     Executes a task and an action directly after.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <param name="task">The task to execute.</param>
    /// <param name="then">The follow up action.</param>
    /// <param name="continueOnCapturedContext">
    ///     Defines if to continue on the caller context before execute the follow up
    ///     action.
    /// </param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">then cannot be null.</exception>
    public static async void FireAndForget(this Task task, Action then, bool continueOnCapturedContext = true)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(then);

        await task.ConfigureAwait(continueOnCapturedContext);
        then();
    }

    /// <summary>
    ///     Executes a task and another task directly after.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <param name="task">The task to execute.</param>
    /// <param name="then">The follow up task.</param>
    /// <param name="continueOnCapturedContext">
    ///     Defines if to continue on the caller context before execute the follow up
    ///     action.
    /// </param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">then cannot be null.</exception>
    public static async void FireAndForget(this Task task, Func<Task> then, bool continueOnCapturedContext = true)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(then);

        await task.ConfigureAwait(continueOnCapturedContext);
        await then();
    }

    /// <summary>
    ///     Executes a task and listen for potential exceptions.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <param name="task">The task to execute.</param>
    /// <param name="onError">The exception callback.</param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">onError cannot be null.</exception>
    public static async void FireAndForget(this Task task, Action<Exception> onError)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(onError);

        try
        {
            await task;
        }
        catch (Exception exception)
        {
            onError.Invoke(exception);
        }
    }

    /// <summary>
    ///     Executes a task and an action directly after with listen for potential exceptions.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <param name="task">The task to execute.</param>
    /// <param name="then">The follow up action.</param>
    /// <param name="onError">The exception callback.</param>
    /// <param name="continueOnCapturedContext">
    ///     Defines if to continue on the caller context before execute the follow up
    ///     action.
    /// </param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">then cannot be null.</exception>
    /// <exception cref="ArgumentNullException">onError cannot be null.</exception>
    public static async void FireAndForget(this Task task, Action then, Action<Exception> onError, bool continueOnCapturedContext = true)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(then);
        ArgumentNullException.ThrowIfNull(onError);

        try
        {
            await task.ConfigureAwait(continueOnCapturedContext);
            then();
        }
        catch (Exception exception)
        {
            onError.Invoke(exception);
        }
    }

    /// <summary>
    ///     Executes a task and another task directly after with listen for potential exceptions.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <param name="task">The task to execute.</param>
    /// <param name="then">The follow up task.</param>
    /// <param name="onError">The exception callback.</param>
    /// <param name="continueOnCapturedContext">
    ///     Defines if to continue on the caller context before execute the follow up
    ///     action.
    /// </param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">then cannot be null.</exception>
    /// <exception cref="ArgumentNullException">onError cannot be null.</exception>
    public static async void FireAndForget(this Task task, Func<Task> then, Action<Exception> onError, bool continueOnCapturedContext = true)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(then);
        ArgumentNullException.ThrowIfNull(onError);

        try
        {
            await task.ConfigureAwait(continueOnCapturedContext);
            await then();
        }
        catch (Exception exception)
        {
            onError.Invoke(exception);
        }
    }

    /// <summary>
    ///     Executes a task and an action directly after with the result from the task.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <typeparam name="T">The task return type.</typeparam>
    /// <param name="task">The task to execute.</param>
    /// <param name="then">The follow up action with the given task return.</param>
    /// <param name="continueOnCapturedContext">
    ///     Defines if to continue on the caller context before execute the follow up
    ///     action.
    /// </param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">then cannot be null.</exception>
    public static async void FireAndForget<T>(this Task<T> task, Action<T> then, bool continueOnCapturedContext = true)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(then);

        var result = await task.ConfigureAwait(continueOnCapturedContext);
        then(result);
    }

    /// <summary>
    ///     Executes a task and another task directly after with the result from the task.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <typeparam name="T">The task return type.</typeparam>
    /// <param name="task">The task to execute.</param>
    /// <param name="then">The follow up task with the given task return.</param>
    /// <param name="continueOnCapturedContext">
    ///     Defines if to continue on the caller context before execute the follow up
    ///     action.
    /// </param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">then cannot be null.</exception>
    public static async void FireAndForget<T>(this Task<T> task, Func<T, Task> then, bool continueOnCapturedContext = true)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(then);

        var result = await task.ConfigureAwait(continueOnCapturedContext);
        await then(result);
    }

    /// <summary>
    ///     Executes a task and an action directly after with the result from the task with listen for potential exceptions.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <typeparam name="T">The task return type.</typeparam>
    /// <param name="task">The task to execute.</param>
    /// <param name="then">The follow up action with the given task return.</param>
    /// <param name="onError">The exception callback.</param>
    /// <param name="continueOnCapturedContext">
    ///     Defines if to continue on the caller context before execute the follow up
    ///     action.
    /// </param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">then cannot be null.</exception>
    /// <exception cref="ArgumentNullException">onError cannot be null.</exception>
    public static async void FireAndForget<T>(this Task<T> task, Action<T> then, Action<Exception> onError, bool continueOnCapturedContext = true)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(then);
        ArgumentNullException.ThrowIfNull(onError);

        try
        {
            var result = await task.ConfigureAwait(continueOnCapturedContext);
            then(result);
        }
        catch (Exception exception)
        {
            onError.Invoke(exception);
        }
    }

    /// <summary>
    ///     Executes a task and another task directly after with the result from the task with listen for potential exceptions.
    ///     Use this to show that you want to execute a task without to wait for its result. (async void)
    /// </summary>
    /// <typeparam name="T">The task return type.</typeparam>
    /// <param name="task">The task to execute.</param>
    /// <param name="then">The follow up task with the given task return.</param>
    /// <param name="onError">The exception callback.</param>
    /// <param name="continueOnCapturedContext">
    ///     Defines if to continue on the caller context before execute the follow up
    ///     action.
    /// </param>
    /// <exception cref="ArgumentNullException">task cannot be null.</exception>
    /// <exception cref="ArgumentNullException">then cannot be null.</exception>
    /// <exception cref="ArgumentNullException">onError cannot be null.</exception>
    public static async void FireAndForget<T>(this Task<T> task, Func<T, Task> then, Action<Exception> onError, bool continueOnCapturedContext = true)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(then);
        ArgumentNullException.ThrowIfNull(onError);

        try
        {
            var result = await task.ConfigureAwait(continueOnCapturedContext);
            await then(result);
        }
        catch (Exception exception)
        {
            onError.Invoke(exception);
        }
    }
}