// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TestableListItem.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

internal class TestableListItem : INotifyPropertyChanging, INotifyPropertyChanged
{
    public bool HasPropertyChangingSubscriber => PropertyChanging != null;

    public bool HasPropertyChangedSubscriber => PropertyChanging != null;

    public event PropertyChangedEventHandler PropertyChanged;

    public event PropertyChangingEventHandler PropertyChanging;

    public void RaisePropertyChanging(string propertyName = null)
    {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }

    public void RaisePropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}