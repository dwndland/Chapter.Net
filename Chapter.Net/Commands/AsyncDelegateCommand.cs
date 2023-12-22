// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncDelegateCommand.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Provides a way to call an async callback from an <see cref="ICommand" />.
/// </summary>
public sealed class AsyncDelegateCommand : IDelegateCommand
{
    private readonly Func<bool> _canExecuteCallback;
    private readonly Func<Task> _executeCallback;
    private bool _isBusy;

    /// <summary>
    ///     Creates a new instance of <see cref="AsyncDelegateCommand" />.
    /// </summary>
    /// <param name="executeCallback">The async callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public AsyncDelegateCommand(Func<Task> executeCallback)
        : this(() => true, executeCallback)
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="AsyncDelegateCommand" />.
    /// </summary>
    /// <param name="canExecuteCallback">The callback to check if the command can be executed.</param>
    /// <param name="executeCallback">The async callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">canExecuteCallback is null</exception>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public AsyncDelegateCommand(Func<bool> canExecuteCallback, Func<Task> executeCallback)
    {
        _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
        _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
    }

    /// <summary>
    ///     Checks if the async command can be executed.
    /// </summary>
    /// <param name="parameter">unused</param>
    /// <returns>True if the async command can be executed; otherwise false.</returns>
    public bool CanExecute(object parameter)
    {
        return !_isBusy && _canExecuteCallback();
    }

    /// <summary>
    ///     Executes the async callback.
    /// </summary>
    /// <param name="parameter">unused</param>
    public void Execute(object parameter)
    {
        ExecuteAsync().FireAndForget();
    }

    /// <summary>
    ///     Raised if the <see cref="CanExecute()" /> needs to be checked again.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    ///     Raises the <see cref="CanExecuteChanged" /> to have the <see cref="CanExecute()" /> checked again.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///     Checks if the async command can be executed.
    /// </summary>
    /// <returns>True if the async command can be executed; otherwise false.</returns>
    public bool CanExecute()
    {
        return !_isBusy && _canExecuteCallback();
    }

    /// <summary>
    ///     Executes the async callback.
    /// </summary>
    public void Execute()
    {
        ExecuteAsync().FireAndForget();
    }

    /// <summary>
    ///     Executes the callback async.
    /// </summary>
    public async Task ExecuteAsync()
    {
        _isBusy = true;
        RaiseCanExecuteChanged();
        await _executeCallback();
        _isBusy = false;
        RaiseCanExecuteChanged();
    }
}

/// <summary>
///     Provides a way to call an async callback from an <see cref="ICommand" />.
/// </summary>
/// <typeparam name="T">The command parameter type.</typeparam>
public sealed class AsyncDelegateCommand<T> : IDelegateCommand
{
    private readonly Func<T, bool> _canExecuteCallback;
    private readonly Func<T, Task> _executeCallback;
    private bool _isBusy;

    /// <summary>
    ///     Creates a new instance of <see cref="AsyncDelegateCommand{T}" />.
    /// </summary>
    /// <param name="executeCallback">The async callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public AsyncDelegateCommand(Func<T, Task> executeCallback)
        : this(_ => true, executeCallback)
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="AsyncDelegateCommand{T}" />.
    /// </summary>
    /// <param name="canExecuteCallback">The callback to check if the command can be executed.</param>
    /// <param name="executeCallback">The async callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">canExecuteCallback is null</exception>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public AsyncDelegateCommand(Func<T, bool> canExecuteCallback, Func<T, Task> executeCallback)
    {
        _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
        _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
    }

    /// <summary>
    ///     Checks if the async command can be executed.
    /// </summary>
    /// <param name="parameter">The command parameter cast to the parameter type.</param>
    /// <returns>True if the async command can be executed; otherwise false.</returns>
    public bool CanExecute(object parameter)
    {
        return !_isBusy && _canExecuteCallback((T)parameter);
    }

    /// <summary>
    ///     Executes the async callback.
    /// </summary>
    /// <param name="parameter">The command parameter cast to the parameter type.</param>
    public void Execute(object parameter)
    {
        ExecuteAsync(parameter).FireAndForget();
    }

    /// <summary>
    ///     Raised if the <see cref="CanExecute" /> needs to be checked again.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    ///     Raises the <see cref="CanExecuteChanged" /> to have the <see cref="CanExecute" /> checked again.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///     Executes the callback async.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    public async Task ExecuteAsync(object parameter)
    {
        _isBusy = true;
        RaiseCanExecuteChanged();
        await _executeCallback((T)parameter);
        _isBusy = false;
        RaiseCanExecuteChanged();
    }
}