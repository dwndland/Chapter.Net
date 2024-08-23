// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateCommand.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Provides a way to call a callback from an <see cref="ICommand" />.
/// </summary>
public sealed class DelegateCommand : IDelegateCommand
{
    private readonly Func<bool> _canExecuteCallback;
    private readonly Action _executeCallback;

    /// <summary>
    ///     Creates a new instance of <see cref="DelegateCommand" />.
    /// </summary>
    /// <param name="executeCallback">The callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public DelegateCommand(Action executeCallback)
        : this(() => true, executeCallback)
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="DelegateCommand" />.
    /// </summary>
    /// <param name="canExecuteCallback">The callback to check if the command can be executed.</param>
    /// <param name="executeCallback">The callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">canExecuteCallback cannot be null</exception>
    /// <exception cref="ArgumentNullException">executeCallback cannot be null</exception>
    public DelegateCommand(Func<bool> canExecuteCallback, Action executeCallback)
    {
        _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
        _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
    }

    /// <summary>
    ///     Checks if the command can be executed.
    /// </summary>
    /// <param name="parameter">unused</param>
    /// <returns>True if the command can be executed; otherwise false.</returns>
    public bool CanExecute(object parameter)
    {
        return _canExecuteCallback();
    }

    /// <summary>
    ///     Executes the callback.
    /// </summary>
    /// <param name="parameter">unused</param>
    public void Execute(object parameter)
    {
        _executeCallback();
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
    ///     Checks if the command can be executed.
    /// </summary>
    /// <returns>True if the command can be executed; otherwise false.</returns>
    public bool CanExecute()
    {
        return _canExecuteCallback();
    }

    /// <summary>
    ///     Executes the callback.
    /// </summary>
    public void Execute()
    {
        _executeCallback();
    }
}

/// <summary>
///     Provides a way to call a callback from an <see cref="ICommand" />.
/// </summary>
/// <typeparam name="T">The command parameter type.</typeparam>
public sealed class DelegateCommand<T> : IDelegateCommand
{
    private readonly Func<T, bool> _canExecuteCallback;
    private readonly Action<T> _executeCallback;

    /// <summary>
    ///     Creates a new instance of <see cref="DelegateCommand{T}" />.
    /// </summary>
    /// <param name="executeCallback">The callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public DelegateCommand(Action<T> executeCallback)
        : this(_ => true, executeCallback)
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="DelegateCommand{T}" />.
    /// </summary>
    /// <param name="canExecuteCallback">The callback to check if the command can be executed.</param>
    /// <param name="executeCallback">The callback to execute if the command is triggered.</param>
    /// <exception cref="ArgumentNullException">canExecuteCallback is null</exception>
    /// <exception cref="ArgumentNullException">executeCallback is null</exception>
    public DelegateCommand(Func<T, bool> canExecuteCallback, Action<T> executeCallback)
    {
        _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
        _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
    }

    /// <summary>
    ///     Checks if the command can be executed.
    /// </summary>
    /// <param name="parameter">The command parameter cast to the parameter type.</param>
    /// <returns>True if the command can be executed; otherwise false.</returns>
    public bool CanExecute(object parameter)
    {
        return _canExecuteCallback((T)parameter);
    }

    /// <summary>
    ///     Executes the callback.
    /// </summary>
    /// <param name="parameter">The command parameter cast to the parameter type.</param>
    public void Execute(object parameter)
    {
        _executeCallback((T)parameter);
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
}