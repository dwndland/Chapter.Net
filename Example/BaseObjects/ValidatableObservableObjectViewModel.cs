// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableObservableObjectViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class ValidatableObservableObjectViewModel : ValidatableObservableObject
{
    private string _validatedValue = string.Empty;

    public string ValidatedValue
    {
        get => _validatedValue;
        set
        {
            NotifyAndSetIfChanged(ref _validatedValue, value);
            Evaluate(_validatedValue == "Hello", "Wrong Word", nameof(ValidatedValue));
        }
    }
}