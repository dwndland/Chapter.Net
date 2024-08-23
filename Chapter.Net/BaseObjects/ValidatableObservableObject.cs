// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableObservableObject.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Is a base class for objects implementing the <see cref="INotifyPropertyChanging" />,
///     <see cref="INotifyPropertyChanged" /> and <see cref="INotifyDataErrorInfo" />.
/// </summary>
public abstract class ValidatableObservableObject : ObservableObject, INotifyDataErrorInfo
{
    private readonly NotifyDataErrorInfo _errors;

    /// <summary>
    ///     Creates a new instance of <see cref="ValidatableObservableObject" />.
    /// </summary>
    protected ValidatableObservableObject()
    {
        _errors = new NotifyDataErrorInfo(NotifyPropertyChanged);
    }

    /// <summary>
    ///     Gets the recorded errors for a given property.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>The recorded errors for a given property if any; otherwise an empty list.</returns>
    public IEnumerable GetErrors(string propertyName)
    {
        return _errors.GetErrors(propertyName);
    }

    /// <summary>
    ///     Gets a value indicating if there are any errors.
    /// </summary>
    public bool HasErrors => _errors.HasErrors;

    /// <summary>
    ///     Raised if the errors for a property has been changed.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
    {
        add => _errors.ErrorsChanged += value;
        remove => _errors.ErrorsChanged -= value;
    }

    /// <summary>
    ///     Updates the state of the given error.
    /// </summary>
    /// <param name="isValid">The indicator if the property is valid. If true all errors for the property will be cleared.</param>
    /// <param name="errors">The errors to add in the case isValid is false.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    protected void Evaluate(bool isValid, IEnumerable<string> errors, string propertyName)
    {
        _errors.Evaluate(isValid, errors, propertyName);
    }

    /// <summary>
    ///     Updates the state of the given error.
    /// </summary>
    /// <param name="isValid">The indicator if the property is valid. If true all errors for the property will be cleared.</param>
    /// <param name="error">The error to add in the case isValid is false.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    protected void Evaluate(bool isValid, string error, string propertyName)
    {
        _errors.Evaluate(isValid, error, propertyName);
    }

    /// <summary>
    ///     Adds a list of errors to the given property.
    /// </summary>
    /// <param name="errors">The errors to add.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    protected void AddErrors(IEnumerable<string> errors, string propertyName)
    {
        _errors.AddErrors(errors, propertyName);
    }

    /// <summary>
    ///     Adds an error to the given property.
    /// </summary>
    /// <param name="error">The error to add.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    protected void AddError(string error, string propertyName)
    {
        _errors.AddError(error, propertyName);
    }

    /// <summary>
    ///     Removes all given errors from the given property.
    /// </summary>
    /// <param name="errors">The errors to remove.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    protected void RemoveErrors(IEnumerable<string> errors, string propertyName)
    {
        _errors.RemoveErrors(errors, propertyName);
    }

    /// <summary>
    ///     Removes the given error from the given property.
    /// </summary>
    /// <param name="error">The error to remove.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <exception cref="ArgumentNullException">errors cannot be null.</exception>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    protected void RemoveError(string error, string propertyName)
    {
        _errors.RemoveError(error, propertyName);
    }

    /// <summary>
    ///     Clears all errors for the given property.
    /// </summary>
    /// <param name="propertyName">The name of the validated property.</param>
    /// <exception cref="ArgumentNullException">propertyName cannot be null.</exception>
    protected void ResetErrors(string propertyName)
    {
        _errors.ResetErrors(propertyName);
    }

    /// <summary>
    ///     Clears all errors for all properties.
    /// </summary>
    protected void ResetAllErrors()
    {
        _errors.ResetAllErrors();
    }
}