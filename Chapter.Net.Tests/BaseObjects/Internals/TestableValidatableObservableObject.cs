// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TestableValidatableObservableObject.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

internal class TestableValidatableObservableObject : ValidatableObservableObject
{
    public void CallEvaluate(bool isValid, IEnumerable<string> errors, string propertyName)
    {
        Evaluate(isValid, errors, propertyName);
    }

    public void CallEvaluate(bool isValid, string error, string propertyName)
    {
        Evaluate(isValid, error, propertyName);
    }

    public void CallAddErrors(IEnumerable<string> errors, string propertyName)
    {
        AddErrors(errors, propertyName);
    }

    public void CallAddError(string error, string propertyName)
    {
        AddError(error, propertyName);
    }

    public void CallRemoveErrors(IEnumerable<string> errors, string propertyName)
    {
        RemoveErrors(errors, propertyName);
    }

    public void CallRemoveError(string error, string propertyName)
    {
        RemoveError(error, propertyName);
    }

    public void CallResetErrors(string propertyName)
    {
        ResetErrors(propertyName);
    }

    public void CallResetAllErrors()
    {
        ResetAllErrors();
    }
}