// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyDataErrorInfo.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Brings helper for an easy maintain of property input validations.
/// </summary>
public class NotifyDataErrorInfo : INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errors;
    private readonly Action<string> _propertyChangedCallback;

    /// <summary>
    ///     Creates a new instance of <see cref="NotifyDataErrorInfo" />.
    /// </summary>
    /// <param name="propertyChangedCallback">The callback to raise property changed from the owner object.</param>
    /// <exception cref="ArgumentNullException">propertyChangedCallback cannot be null.</exception>
    public NotifyDataErrorInfo(Action<string> propertyChangedCallback)
    {
        _propertyChangedCallback = propertyChangedCallback ?? throw new ArgumentNullException(nameof(propertyChangedCallback));
        _errors = new Dictionary<string, List<string>>();
    }

    /// <summary>
    ///     Gets the recorded errors for a given property.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>The recorded errors for a given property if any; otherwise an empty list.</returns>
    public IEnumerable GetErrors(string propertyName)
    {
        if (propertyName == null)
            return Array.Empty<string>();

        if (_errors.TryGetValue(propertyName, out var errors))
            return errors;
        return Array.Empty<string>();
    }

    /// <summary>
    ///     Gets a value indicating if there are any errors.
    /// </summary>
    public bool HasErrors => _errors.Count > 0;

    /// <summary>
    ///     Raised if the errors for a property has been changed.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    /// <summary>
    ///     Updates the state of the given error.
    /// </summary>
    /// <param name="isValid">The indicator if the property is valid. If true all errors for the property will be cleared.</param>
    /// <param name="errors">The errors to add in the case isValid is false.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    public void Evaluate(bool isValid, IEnumerable<string> errors, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(errors);
        ArgumentNullException.ThrowIfNull(propertyName);

        if (isValid)
            ResetErrors(propertyName);
        else
            AddErrors(errors, propertyName);
    }

    /// <summary>
    ///     Updates the state of the given error.
    /// </summary>
    /// <param name="isValid">The indicator if the property is valid. If true all errors for the property will be cleared.</param>
    /// <param name="error">The error to add in the case isValid is false.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">error cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    public void Evaluate(bool isValid, string error, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(error);
        ArgumentNullException.ThrowIfNull(propertyName);

        if (isValid)
            ResetErrors(propertyName);
        else
            AddError(error, propertyName);
    }

    /// <summary>
    ///     Adds a list of errors to the given property.
    /// </summary>
    /// <param name="errors">The errors to add.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    public void AddErrors(IEnumerable<string> errors, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(errors);
        ArgumentNullException.ThrowIfNull(propertyName);

        if (!_errors.ContainsKey(propertyName))
            _errors[propertyName] = [];
        foreach (var error in errors)
            if (!_errors[propertyName].Contains(error))
                _errors[propertyName].Add(error);
        OnErrorsChanged(propertyName);
    }

    /// <summary>
    ///     Adds an error to the given property.
    /// </summary>
    /// <param name="error">The error to add.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">error cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    public void AddError(string error, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(error);
        ArgumentNullException.ThrowIfNull(propertyName);

        if (!_errors.ContainsKey(propertyName))
            _errors[propertyName] = [];
        if (!_errors[propertyName].Contains(error))
            _errors[propertyName].Add(error);
        OnErrorsChanged(propertyName);
    }

    /// <summary>
    ///     Removes all given errors from the given property.
    /// </summary>
    /// <param name="errors">The errors to remove.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    public void RemoveErrors(IEnumerable<string> errors, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(errors);
        ArgumentNullException.ThrowIfNull(propertyName);

        if (!_errors.TryGetValue(propertyName, out var errorsContainer))
            return;

        foreach (var error in errors)
            errorsContainer.Remove(error);
        OnErrorsChanged(propertyName);
    }

    /// <summary>
    ///     Removes the given error from the given property.
    /// </summary>
    /// <param name="error">The error to remove.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">error cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    public void RemoveError(string error, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(error);
        ArgumentNullException.ThrowIfNull(propertyName);

        if (!_errors.TryGetValue(propertyName, out var errorsContainer))
            return;

        errorsContainer.Remove(error);
        OnErrorsChanged(propertyName);
    }

    /// <summary>
    ///     Clears all errors for the given property.
    /// </summary>
    /// <param name="propertyName">The name of the validated property.</param>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    public void ResetErrors(string propertyName)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        _errors.Remove(propertyName);
        OnErrorsChanged(propertyName);
    }

    /// <summary>
    ///     Clears all errors for all properties.
    /// </summary>
    public void ResetAllErrors()
    {
        var propertyNames = _errors.Keys.ToList();
        _errors.Clear();
        foreach (var propertyName in propertyNames)
            OnErrorsChanged(propertyName);
    }

    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        _propertyChangedCallback.Invoke(propertyName);
    }
}