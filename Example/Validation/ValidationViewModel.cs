// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class ValidationViewModel : ObservableObject, INotifyDataErrorInfo
{
    private readonly NotifyDataErrorInfo _errors = new();
    private string _validatedValue = string.Empty;

    public string ValidatedValue
    {
        get => _validatedValue;
        set
        {
            NotifyAndSetIfChanged(ref _validatedValue, value);
            _errors.Evaluate(_validatedValue == "Hello", "Wrong Word", nameof(ValidatedValue));
        }
    }

    public IEnumerable GetErrors(string propertyName)
    {
        return _errors.GetErrors(propertyName);
    }

    public bool HasErrors => _errors.HasErrors;

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
    {
        add => _errors.ErrorsChanged += value;
        remove => _errors.ErrorsChanged -= value;
    }
}