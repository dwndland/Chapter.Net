// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableObjectTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class ObservableObjectTests
{
    private TestableObservableObject _target;

    [SetUp]
    public void SetUp()
    {
        _target = new TestableObservableObject();
    }

    [Test]
    public void NotifyPropertyChanging_CalledWithoutPropertyName_RaisesPropertyChangingWithRightPropertyName()
    {
        var triggered = false;
        _target.PropertyChanging += TargetOnPropertyChanging;

        _target.SelfWithoutPropertyName = "Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            if (e.PropertyName == nameof(TestableObservableObject.SelfWithoutPropertyName))
                triggered = true;
        }
    }

    [Test]
    public void NotifyPropertyChanged_CalledWithoutPropertyName_RaisesPropertyChangedWithRightPropertyName()
    {
        var triggered = false;
        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.SelfWithoutPropertyName = "Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            if (e.PropertyName == nameof(TestableObservableObject.SelfWithoutPropertyName))
                triggered = true;
        }
    }

    [Test]
    public void NotifyPropertyChanging_CalledWithPropertyName_RaisesPropertyChangingWithGivenPropertyName()
    {
        var triggered = false;
        _target.PropertyChanging += TargetOnPropertyChanging;

        _target.SelfWithPropertyName = "Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            if (e.PropertyName == "PropertyName")
                triggered = true;
        }
    }

    [Test]
    public void NotifyPropertyChanged_CalledWithPropertyName_RaisesPropertyChangedWithGivenPropertyName()
    {
        var triggered = false;
        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.SelfWithPropertyName = "Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            if (e.PropertyName == "PropertyName")
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithUnchangedValueWithoutPropertyName_RaisesPropertyChangingWithRightPropertyName()
    {
        var triggered = false;
        _target.BaseSetWithoutPropertyName = "Test";

        _target.PropertyChanging += TargetOnPropertyChanging;

        _target.BaseSetWithoutPropertyName = "Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            if (e.PropertyName == nameof(TestableObservableObject.BaseSetWithoutPropertyName))
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithUnchangedValueWithoutPropertyName_RaisesPropertyChangedWithRightPropertyName()
    {
        var triggered = false;
        _target.BaseSetWithoutPropertyName = "Test";

        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.BaseSetWithoutPropertyName = "Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            if (e.PropertyName == nameof(TestableObservableObject.BaseSetWithoutPropertyName))
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithUnchangedValueWithPropertyName_RaisesPropertyChangingWithGivenPropertyName()
    {
        var triggered = false;
        _target.BaseSetWithPropertyName = "Test";

        _target.PropertyChanging += TargetOnPropertyChanging;

        _target.BaseSetWithPropertyName = "Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            if (e.PropertyName == "PropertyName")
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithUnchangedValueWithPropertyName_RaisesPropertyChangedWithGivenPropertyName()
    {
        var triggered = false;
        _target.BaseSetWithPropertyName = "Test";

        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.BaseSetWithPropertyName = "Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            if (e.PropertyName == "PropertyName")
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithChangedValueWithoutPropertyName_RaisesPropertyChangingWithRightPropertyName()
    {
        var triggered = false;
        _target.BaseSetWithoutPropertyName = "Test";

        _target.PropertyChanging += TargetOnPropertyChanging;

        _target.BaseSetWithoutPropertyName = "Neu Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            if (e.PropertyName == nameof(TestableObservableObject.BaseSetWithoutPropertyName))
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithChangedValueWithoutPropertyName_RaisesPropertyChangedWithRightPropertyName()
    {
        var triggered = false;
        _target.BaseSetWithoutPropertyName = "Test";

        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.BaseSetWithoutPropertyName = "Neu Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            if (e.PropertyName == nameof(TestableObservableObject.BaseSetWithoutPropertyName))
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithChangedValueWithPropertyName_RaisesPropertyChangingWithRightPropertyName()
    {
        var triggered = false;
        _target.BaseSetWithPropertyName = "Test";

        _target.PropertyChanging += TargetOnPropertyChanging;

        _target.BaseSetWithPropertyName = "Neu Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            if (e.PropertyName == "PropertyName")
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithChangedValueWithPropertyName_RaisesPropertyChangedWithRightPropertyName()
    {
        var triggered = false;
        _target.BaseSetWithPropertyName = "Test";

        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.BaseSetWithPropertyName = "Neu Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            if (e.PropertyName == "PropertyName")
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSet_CalledWithChangedValueWithoutPropertyName_SetsTheBackingField()
    {
        _target.BaseSetWithoutPropertyName = "Test";
        _target.BaseSetWithoutPropertyName = "Neu Test";

        Assert.That(_target.BaseSetWithoutPropertyName, Is.EqualTo("Neu Test"));
    }

    [Test]
    public void NotifyAndSet_CalledWithChangedValueWithPropertyName_SetsTheBackingField()
    {
        _target.BaseSetWithPropertyName = "Test";
        _target.BaseSetWithPropertyName = "Neu Test";

        Assert.That(_target.BaseSetWithPropertyName, Is.EqualTo("Neu Test"));
    }

    [Test]
    public void NotifyAndSetIfChanged_CalledWithUnchangedValueWithoutPropertyName_RaisesNothing()
    {
        _target.BaseSetIfChangedWithoutPropertyName = "Test";

        _target.PropertyChanging += TargetOnPropertyChanging;
        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.BaseSetIfChangedWithoutPropertyName = "Test";
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            Assert.Fail();
        }

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            Assert.Fail();
        }
    }

    [Test]
    public void NotifyAndSetIfChanged_CalledWithUnchangedValueWithPropertyName_RaisesNothing()
    {
        _target.BaseSetIfChangedWithPropertyName = "Test";

        _target.PropertyChanging += TargetOnPropertyChanging;
        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.BaseSetIfChangedWithPropertyName = "Test";
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            Assert.Fail();
        }

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            Assert.Fail();
        }
    }

    [Test]
    public void NotifyAndSetIfChanged_CalledWithChangedValueWithoutPropertyName_RaisesPropertyChangingWithRightPropertyName()
    {
        var triggered = false;
        _target.BaseSetIfChangedWithoutPropertyName = "Test";

        _target.PropertyChanging += TargetOnPropertyChanging;

        _target.BaseSetIfChangedWithoutPropertyName = "New Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            if (e.PropertyName == nameof(TestableObservableObject.BaseSetIfChangedWithoutPropertyName))
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSetIfChanged_CalledWithChangedValueWithoutPropertyName_RaisesPropertyChangedWithRightPropertyName()
    {
        var triggered = false;
        _target.BaseSetIfChangedWithoutPropertyName = "Test";

        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.BaseSetIfChangedWithoutPropertyName = "New Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            if (e.PropertyName == nameof(TestableObservableObject.BaseSetIfChangedWithoutPropertyName))
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSetIfChanged_CalledWithChangedValueWithPropertyName_RaisesPropertyChangingWithGivenPropertyName()
    {
        var triggered = false;
        _target.BaseSetIfChangedWithPropertyName = "Test";

        _target.PropertyChanging += TargetOnPropertyChanging;

        _target.BaseSetIfChangedWithPropertyName = "New Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            _target.PropertyChanging -= TargetOnPropertyChanging;
            if (e.PropertyName == "PropertyName")
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSetIfChanged_CalledWithChangedValueWithPropertyName_RaisesPropertyChangedWithGivenPropertyName()
    {
        var triggered = false;
        _target.BaseSetIfChangedWithPropertyName = "Test";

        _target.PropertyChanged += TargetOnPropertyChanged;

        _target.BaseSetIfChangedWithPropertyName = "New Test";

        Assert.That(triggered, Is.True);
        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _target.PropertyChanged -= TargetOnPropertyChanged;
            if (e.PropertyName == "PropertyName")
                triggered = true;
        }
    }

    [Test]
    public void NotifyAndSetIfChanged_CalledWithChangedValueWithoutPropertyName_SetsTheBackingField()
    {
        _target.BaseSetIfChangedWithoutPropertyName = "Test";
        _target.BaseSetIfChangedWithoutPropertyName = "Neu Test";

        Assert.That(_target.BaseSetIfChangedWithoutPropertyName, Is.EqualTo("Neu Test"));
    }

    [Test]
    public void NotifyAndSetIfChanged_CalledWithChangedValueWithPropertyName_SetsTheBackingField()
    {
        _target.BaseSetIfChangedWithPropertyName = "Test";
        _target.BaseSetIfChangedWithPropertyName = "Neu Test";

        Assert.That(_target.BaseSetIfChangedWithPropertyName, Is.EqualTo("Neu Test"));
    }
}