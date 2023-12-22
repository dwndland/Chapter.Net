// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableObservableObjectTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class ValidatableObservableObjectTests
{
    private TestableValidatableObservableObject _target;

    [SetUp]
    public void SetUp()
    {
        _target = new TestableValidatableObservableObject();
    }

    [Test]
    public void Class_Created_IsAnObservableObject()
    {
        Assert.That(_target, Is.InstanceOf<ObservableObject>());
    }

    [Test]
    public void GetErrors_CalledWithNull_ReturnsEmptyCollection()
    {
        var errors = _target.GetErrors(null).Cast<string>().ToList();

        Assert.That(errors, Is.Empty);
    }

    [Test]
    public void GetErrors_ErrorsAreInList_ReturnsAllAddedErrors()
    {
        _target.CallAddError("First", "PropertyName1");
        _target.CallAddError("Second", "PropertyName1");
        _target.CallAddError("First", "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Has.Count.EqualTo(2));
            Assert.That(errors1[0], Is.EqualTo("First"));
            Assert.That(errors1[1], Is.EqualTo("Second"));
        });
        Assert.Multiple(() =>
        {
            Assert.That(errors2, Has.Count.EqualTo(1));
            Assert.That(errors2[0], Is.EqualTo("First"));
        });
    }

    [Test]
    public void GetErrors_ErrorsAreNotList_ReturnsEmptyCollection()
    {
        _target.CallAddError("First", "PropertyName1");

        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.That(errors2, Is.Empty);
    }

    [Test]
    public void HasErrors_ErrorsAreInList_ReturnsTrue()
    {
        _target.CallAddError("First", "PropertyName1");

        var hasErrors = _target.HasErrors;

        Assert.That(hasErrors, Is.True);
    }

    [Test]
    public void HasErrors_ErrorsAreNotInList_ReturnsFalse()
    {
        var hasErrors = _target.HasErrors;

        Assert.That(hasErrors, Is.False);
    }

    [Test]
    public void Evaluate_ErrorsCalledWithNullErrors_RaisesException()
    {
        Assert.That(() => _target.CallEvaluate(true, (List<string>)null, ""), Throws.ArgumentNullException);
    }

    [Test]
    public void Evaluate_ErrorsCalledWithNullPropertyName_RaisesException()
    {
        Assert.That(() => _target.CallEvaluate(true, Array.Empty<string>(), null), Throws.ArgumentNullException);
    }

    [Test]
    public void Evaluate_CalledWithFalseWithSomeErrorMessages_AddsItToTheListForTheGivenProperty()
    {
        _target.CallEvaluate(false, new[] { "First" }, "PropertyName1");
        _target.CallEvaluate(false, new[] { "Second" }, "PropertyName1");
        _target.CallEvaluate(false, new[] { "First" }, "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Has.Count.EqualTo(2));
            Assert.That(errors1[0], Is.EqualTo("First"));
            Assert.That(errors1[1], Is.EqualTo("Second"));
        });
        Assert.Multiple(() =>
        {
            Assert.That(errors2, Has.Count.EqualTo(1));
            Assert.That(errors2[0], Is.EqualTo("First"));
        });
    }

    [Test]
    public void Evaluate_CalledWithFalseWithSomeErrorMessages_RaisesErrorChangedEvent()
    {
        TestErrorChanged(() => _target.CallEvaluate(false, new[] { "First" }, "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallEvaluate(false, new[] { "Second" }, "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallEvaluate(false, new[] { "First" }, "PropertyName2"), "PropertyName2");
    }

    [Test]
    public void Evaluate_CalledWithTrueWithSomeErrorMessages_RemovesTheErrorFromTheListForTheGivenProperty()
    {
        _target.CallEvaluate(false, new[] { "First" }, "PropertyName1");
        _target.CallEvaluate(false, new[] { "Second" }, "PropertyName1");
        _target.CallEvaluate(false, new[] { "First" }, "PropertyName2");

        _target.CallEvaluate(true, new[] { "Ignored" }, "PropertyName1");
        _target.CallEvaluate(true, new[] { "Ignored" }, "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();
        Assert.Multiple(() =>
        {
            Assert.That(errors1, Is.Empty);
            Assert.That(errors2, Is.Empty);
        });
    }

    [Test]
    public void Evaluate_CalledWithTrueWithSomeErrorMessages_RaisesErrorChangedEvent()
    {
        _target.CallEvaluate(false, new[] { "First" }, "PropertyName1");
        _target.CallEvaluate(false, new[] { "Second" }, "PropertyName1");
        _target.CallEvaluate(false, new[] { "First" }, "PropertyName2");

        TestErrorChanged(() => _target.CallEvaluate(true, new[] { "Ignored" }, "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallEvaluate(true, new[] { "Ignored" }, "PropertyName2"), "PropertyName2");
    }

    [Test]
    public void Evaluate_ErrorCalledWithNullErrors_RaisesException()
    {
        Assert.That(() => _target.CallEvaluate(true, (string)null, ""), Throws.ArgumentNullException);
    }

    [Test]
    public void Evaluate_ErrorCalledWithNullPropertyName_RaisesException()
    {
        Assert.That(() => _target.CallEvaluate(true, "", null), Throws.ArgumentNullException);
    }

    [Test]
    public void Evaluate_CalledWithFalseWithErrorMessage_AddsItToTheListForTheGivenProperty()
    {
        _target.CallEvaluate(false, "First", "PropertyName1");
        _target.CallEvaluate(false, "Second", "PropertyName1");
        _target.CallEvaluate(false, "First", "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Has.Count.EqualTo(2));
            Assert.That(errors1[0], Is.EqualTo("First"));
            Assert.That(errors1[1], Is.EqualTo("Second"));
        });

        Assert.Multiple(() =>
        {
            Assert.That(errors2, Has.Count.EqualTo(1));
            Assert.That(errors2[0], Is.EqualTo("First"));
        });
    }

    [Test]
    public void Evaluate_CalledWithFalseWithErrorMessage_RaisesErrorChangedEvent()
    {
        TestErrorChanged(() => _target.CallEvaluate(false, "First", "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallEvaluate(false, "Second", "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallEvaluate(false, "First", "PropertyName2"), "PropertyName2");
    }

    [Test]
    public void Evaluate_CalledWithTrueWithErrorMessage_RemovesTheErrorFromTheListForTheGivenProperty()
    {
        _target.CallEvaluate(false, "First", "PropertyName1");
        _target.CallEvaluate(false, "Second", "PropertyName1");
        _target.CallEvaluate(false, "First", "PropertyName2");

        _target.CallEvaluate(true, "Ignored", "PropertyName1");
        _target.CallEvaluate(true, "Ignored", "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Is.Empty);
            Assert.That(errors2, Is.Empty);
        });
    }

    [Test]
    public void Evaluate_CalledWithTrueWithErrorMessage_RaisesErrorChangedEvent()
    {
        _target.CallEvaluate(false, "First", "PropertyName1");
        _target.CallEvaluate(false, "Second", "PropertyName1");
        _target.CallEvaluate(false, "First", "PropertyName2");

        TestErrorChanged(() => _target.CallEvaluate(true, "Ignored", "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallEvaluate(true, "Ignored", "PropertyName2"), "PropertyName2");
    }

    [Test]
    public void AddErrors_CalledWithNullErrors_ThrowsException()
    {
        Assert.That(() => _target.CallAddErrors(null, ""), Throws.ArgumentNullException);
    }

    [Test]
    public void AddErrors_CalledWithNullPropertyName_ThrowsException()
    {
        Assert.That(() => _target.CallAddErrors(Array.Empty<string>(), null), Throws.ArgumentNullException);
    }

    [Test]
    public void AddErrors_CalledWithSomeErrorMessages_AddsItToTheListForTheGivenProperty()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Has.Count.EqualTo(2));
            Assert.That(errors1[0], Is.EqualTo("First"));
            Assert.That(errors1[1], Is.EqualTo("Second"));
        });
        Assert.Multiple(() =>
        {
            Assert.That(errors2, Has.Count.EqualTo(1));
            Assert.That(errors2[0], Is.EqualTo("First"));
        });
    }

    [Test]
    public void AddErrors_CalledWithSomeErrorMessages_RaisesErrorChangedEvent()
    {
        TestErrorChanged(() => _target.CallAddErrors(new[] { "First" }, "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallAddErrors(new[] { "Second" }, "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallAddErrors(new[] { "First" }, "PropertyName2"), "PropertyName2");
    }

    [Test]
    public void AddError_CalledWithNullErrors_ThrowsException()
    {
        Assert.That(() => _target.CallAddError(null, ""), Throws.ArgumentNullException);
    }

    [Test]
    public void AddError_CalledWithNullPropertyName_ThrowsException()
    {
        Assert.That(() => _target.CallAddError("", null), Throws.ArgumentNullException);
    }

    [Test]
    public void AddError_CalledWithSomeErrorMessages_AddsItToTheListForTheGivenProperty()
    {
        _target.CallAddError("First", "PropertyName1");
        _target.CallAddError("Second", "PropertyName1");
        _target.CallAddError("First", "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Has.Count.EqualTo(2));
            Assert.That(errors1[0], Is.EqualTo("First"));
            Assert.That(errors1[1], Is.EqualTo("Second"));
        });
        Assert.Multiple(() =>
        {
            Assert.That(errors2, Has.Count.EqualTo(1));
            Assert.That(errors2[0], Is.EqualTo("First"));
        });
    }

    [Test]
    public void AddError_CalledWithSomeErrorMessages_RaisesErrorChangedEvent()
    {
        TestErrorChanged(() => _target.CallAddError("First", "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallAddError("Second", "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallAddError("First", "PropertyName2"), "PropertyName2");
    }

    [Test]
    public void RemoveErrors_CalledWithNullErrors_ThrowsException()
    {
        Assert.That(() => _target.CallRemoveErrors(null, ""), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveErrors_CalledWithNullPropertyName_ThrowsException()
    {
        Assert.That(() => _target.CallRemoveErrors(Array.Empty<string>(), null), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveErrors_CalledWithSomeErrorMessages_RemovesOnlyThoseFromGivenProperty()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Third" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName2");

        _target.CallRemoveErrors(new[] { "Second", "Third" }, "PropertyName1");
        _target.CallRemoveErrors(new[] { "Second" }, "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Has.Count.EqualTo(1));
            Assert.That(errors1[0], Is.EqualTo("First"));
        });
        Assert.Multiple(() =>
        {
            Assert.That(errors2, Has.Count.EqualTo(1));
            Assert.That(errors2[0], Is.EqualTo("First"));
        });
    }

    [Test]
    public void RemoveErrors_CalledWithSomeErrorMessages_RaisesErrorChangedEvent()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Third" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName2");

        TestErrorChanged(() => _target.CallRemoveErrors(new[] { "Second", "Third" }, "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallRemoveErrors(new[] { "Second" }, "PropertyName2"), "PropertyName2");
    }

    [Test]
    public void RemoveError_CalledWithNullErrors_ThrowsException()
    {
        Assert.That(() => _target.CallRemoveError(null, ""), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveError_CalledWithNullPropertyName_ThrowsException()
    {
        Assert.That(() => _target.CallRemoveError("", null), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveError_CalledWithErrorMessage_RemovesOnlyThoseFromGivenProperty()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Third" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName2");

        _target.CallRemoveError("Second", "PropertyName1");
        _target.CallRemoveError("Third", "PropertyName1");
        _target.CallRemoveError("Second", "PropertyName2");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Has.Count.EqualTo(1));
            Assert.That(errors1[0], Is.EqualTo("First"));
        });
        Assert.Multiple(() =>
        {
            Assert.That(errors2, Has.Count.EqualTo(1));
            Assert.That(errors2[0], Is.EqualTo("First"));
        });
    }

    [Test]
    public void RemoveError_CalledWithErrorMessage_RaisesErrorChangedEvent()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Third" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName2");

        TestErrorChanged(() => _target.CallRemoveError("Second", "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallRemoveError("Third", "PropertyName1"), "PropertyName1");
        TestErrorChanged(() => _target.CallRemoveError("Second", "PropertyName2"), "PropertyName2");
    }

    [Test]
    public void ResetErrors_CalledWithNull_ThrowsException()
    {
        Assert.That(() => _target.CallResetErrors(null), Throws.ArgumentNullException);
    }

    [Test]
    public void ResetErrors_Called_RemovesOnlyThoseFromGivenProperty()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Third" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName2");

        _target.CallResetErrors("PropertyName1");

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() => { Assert.That(errors1, Is.Empty); });
        Assert.Multiple(() =>
        {
            Assert.That(errors2, Has.Count.EqualTo(2));
            Assert.That(errors2[0], Is.EqualTo("First"));
            Assert.That(errors2[1], Is.EqualTo("Second"));
        });
    }

    [Test]
    public void ResetErrors_Called_RaisesErrorChangedEvent()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Third" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName2");

        TestErrorChanged(() => _target.CallResetErrors("PropertyName1"), "PropertyName1");
    }

    [Test]
    public void ResetAllErrors_Called_RemovesAllErrors()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Third" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName2");

        _target.CallResetAllErrors();

        var errors1 = _target.GetErrors("PropertyName1").Cast<string>().ToList();
        var errors2 = _target.GetErrors("PropertyName2").Cast<string>().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(errors1, Is.Empty);
            Assert.That(errors2, Is.Empty);
        });
    }

    [Test]
    public void ResetAllErrors_Called_RaisesErrorChangedEvent()
    {
        _target.CallAddErrors(new[] { "First" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName1");
        _target.CallAddErrors(new[] { "Third" }, "PropertyName1");
        _target.CallAddErrors(new[] { "First" }, "PropertyName2");
        _target.CallAddErrors(new[] { "Second" }, "PropertyName2");

        TestErrorChanged(() => _target.CallResetAllErrors(), "PropertyName1", "PropertyName2");
    }

    private void TestErrorChanged(Action action, params string[] propertyNames)
    {
        var triggered = false;
        _target.ErrorsChanged += TargetOnErrorsChanged;

        action();

        _target.ErrorsChanged -= TargetOnErrorsChanged;

        Assert.That(triggered, Is.True);
        return;

        void TargetOnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            triggered = propertyNames.Contains(e.PropertyName);
        }
    }
}