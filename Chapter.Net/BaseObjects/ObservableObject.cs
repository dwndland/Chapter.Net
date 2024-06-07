// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableObject.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Is a base class for objects implementing the <see cref="INotifyPropertyChanging" /> and
///     <see cref="INotifyPropertyChanged" />.
/// </summary>
public abstract class ObservableObject : INotifyPropertyChanging, INotifyPropertyChanged
{
    /// <summary>
    ///     Raised if a property has been changed.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     Raised of a property is about to change.
    /// </summary>
    public event PropertyChangingEventHandler PropertyChanging;

    /// <summary>
    ///     Raises the <see cref="PropertyChanging" /> for a specific property.
    /// </summary>
    /// <param name="property">The name of the property which is about to change.</param>
    protected void NotifyPropertyChanging([CallerMemberName] string property = null)
    {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(property));
    }

    /// <summary>
    ///     Raises the <see cref="PropertyChanged" /> for a specific property.
    /// </summary>
    /// <param name="property">The name of the changed property.</param>
    protected void NotifyPropertyChanged([CallerMemberName] string property = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    /// <summary>
    ///     Raises <see cref="PropertyChanging" />, sets the property value and raises <see cref="PropertyChanged" /> after.
    ///     Its independently if the property has been changed or not.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="backingField">The property backing field.</param>
    /// <param name="newValue">The new property value.</param>
    /// <param name="propertyName">The name of the property.</param>
    protected void NotifyAndSet<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
    {
        NotifyPropertyChanging(propertyName);
        backingField = newValue;
        NotifyPropertyChanged(propertyName);
    }

    /// <summary>
    ///     Raises <see cref="PropertyChanging" />, sets the property value and raises <see cref="PropertyChanged" /> after.
    ///     Its done only if the property has been changed.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="backingField">The property backing field.</param>
    /// <param name="newValue">The new property value.</param>
    /// <param name="propertyName">The name of the property.</param>
    protected void NotifyAndSetIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
    {
        if (!Equals(backingField, newValue))
            NotifyAndSet(ref backingField, newValue, propertyName);
    }
}