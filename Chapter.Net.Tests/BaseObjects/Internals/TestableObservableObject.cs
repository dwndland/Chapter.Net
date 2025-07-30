// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TestableObservableObject.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

internal class TestableObservableObject : ObservableObject
{
    private string _baseSetIfChangedWithoutPropertyName;
    private string _baseSetIfChangedWithPropertyName;
    private string _baseSetWithoutPropertyName;
    private string _baseSetWithPropertyName;
    private string _selfWithoutPropertyName;
    private string _selfWithPropertyName;

    public bool NotifyAndSetIfChangedResult { get; set; }

    public string SelfWithoutPropertyName
    {
        get => _selfWithoutPropertyName;
        set
        {
            NotifyPropertyChanging();
            _selfWithoutPropertyName = value;
            NotifyPropertyChanged();
        }
    }

    public string SelfWithPropertyName
    {
        get => _selfWithPropertyName;
        set
        {
            NotifyPropertyChanging("PropertyName");
            _selfWithPropertyName = value;
            NotifyPropertyChanged("PropertyName");
        }
    }

    public string BaseSetWithoutPropertyName
    {
        get => _baseSetWithoutPropertyName;
        set => NotifyAndSet(ref _baseSetWithoutPropertyName, value);
    }

    public string BaseSetWithPropertyName
    {
        get => _baseSetWithPropertyName;
        set => NotifyAndSet(ref _baseSetWithPropertyName, value, "PropertyName");
    }

    public string BaseSetIfChangedWithoutPropertyName
    {
        get => _baseSetIfChangedWithoutPropertyName;
        set => NotifyAndSetIfChangedResult = NotifyAndSetIfChanged(ref _baseSetIfChangedWithoutPropertyName, value);
    }

    public string BaseSetIfChangedWithPropertyName
    {
        get => _baseSetIfChangedWithPropertyName;
        set => NotifyAndSetIfChangedResult = NotifyAndSetIfChanged(ref _baseSetIfChangedWithPropertyName, value, "PropertyName");
    }
}