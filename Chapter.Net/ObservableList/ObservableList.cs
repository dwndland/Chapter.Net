// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableList.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     A list implementing <see cref="INotifyCollectionChanged" />, <see cref="INotifyPropertyChanging" /> and
///     <see cref="INotifyCollectionChanged" />.
/// </summary>
/// <typeparam name="T">The type of the items in the list.</typeparam>
public class ObservableList<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanging, INotifyPropertyChanged
{
    private const string IndexerName = "Item[]";
    private bool _catchPropertyChanged;
    private bool _catchPropertyChanging;
    private DisableNotifications _disableNotify;
    private IInvokator _invokator = new DirectInvokator();

    /// <summary>
    ///     Creates a new instance of <see cref="ObservableList{T}" />.
    /// </summary>
    public ObservableList()
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="ObservableList{T}" />.
    /// </summary>
    /// <param name="invokator">The invokator how to do actions on the list.</param>
    /// <exception cref="ArgumentNullException">invokator cannot be null.</exception>
    public ObservableList(IInvokator invokator)
    {
        Invokator = invokator ?? throw new ArgumentNullException(nameof(invokator));
    }

    /// <summary>
    ///     Creates a new instance of <see cref="ObservableList{T}" />.
    /// </summary>
    /// <param name="collection">The source collection to take over the items from.</param>
    /// <exception cref="ArgumentNullException">collection cannot be null.</exception>
    public ObservableList(IEnumerable<T> collection)
        : base(collection.ToList())
    {
        Invokator = new DirectInvokator();
    }

    /// <summary>
    ///     Creates a new instance of <see cref="ObservableList{T}" />.
    /// </summary>
    /// <param name="invokator">The invokator how to do actions on the list.</param>
    /// <param name="collection">The source collection to take over the items from.</param>
    /// <exception cref="ArgumentNullException">invokator cannot be null.</exception>
    /// <exception cref="ArgumentNullException">collection cannot be null.</exception>
    public ObservableList(IInvokator invokator, IEnumerable<T> collection)
        : base(collection.ToList())
    {
        Invokator = invokator ?? throw new ArgumentNullException(nameof(invokator));
    }

    /// <summary>
    ///     Gets or sets the invokator to use for actions on the collection.
    /// </summary>
    /// <exception cref="ArgumentNullException">value cannot be nulls.</exception>
    public IInvokator Invokator
    {
        get => _invokator;
        set => _invokator = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    ///     Gets or sets the value indicating of the <see cref="INotifyPropertyChanging.PropertyChanging" /> is taken from the
    ///     elements in the collection and forwarded by <see cref="INotifyPropertyChanging" />.
    /// </summary>
    public bool CatchPropertyChanging
    {
        get => _catchPropertyChanging;
        set
        {
            if (_catchPropertyChanging == value)
                return;

            if (value)
            {
                _catchPropertyChanging = value;
                CatchItemPropertyChanging();
            }
            else
            {
                IgnoreItemPropertyChanging();
                _catchPropertyChanging = value;
            }
        }
    }

    /// <summary>
    ///     Gets or sets the value indicating of the <see cref="INotifyPropertyChanged.PropertyChanged" /> is taken from the
    ///     elements in the collection and forwarded by <see cref="ItemPropertyChanged" />.
    /// </summary>
    public bool CatchPropertyChanged
    {
        get => _catchPropertyChanged;
        set
        {
            if (_catchPropertyChanged == value)
                return;

            if (value)
            {
                _catchPropertyChanged = value;
                CatchItemPropertyChanged();
            }
            else
            {
                IgnoreItemPropertyChanged();
                _catchPropertyChanged = value;
            }
        }
    }

    /// <summary>
    ///     Raised of the collection has been changed.
    /// </summary>
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    /// <summary>
    ///     Raised of a property has been changed.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     Raised of a property is about to change.
    /// </summary>
    public event PropertyChangingEventHandler PropertyChanging;

    /// <summary>
    ///     Raised the forwarded <see cref="INotifyPropertyChanging.PropertyChanging" /> from an element of the list.
    /// </summary>
    public event EventHandler<PropertyChangingEventArgs> ItemPropertyChanging;

    /// <summary>
    ///     Raised the forwarded <see cref="INotifyPropertyChanged.PropertyChanged" /> from an element of the list.
    /// </summary>
    public event EventHandler<PropertyChangedEventArgs> ItemPropertyChanged;

    /// <summary>
    ///     Moves an item from one to another position in the list.
    /// </summary>
    /// <param name="fromIndex">The old index of the item.</param>
    /// <param name="toIndex">The new index of the item.</param>
    /// <exception cref="ArgumentOutOfRangeException">fromIndex is out of range.</exception>
    /// <exception cref="ArgumentOutOfRangeException">toIndex is out of range.</exception>
    public virtual void Move(int fromIndex, int toIndex)
    {
        MoveItem(fromIndex, toIndex);
    }

    /// <summary>
    ///     Adds multiple items to the collection.
    /// </summary>
    /// <param name="items">The items to add.</param>
    /// <exception cref="ArgumentNullException">items cannot be null.</exception>
    public virtual void AddRange(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        _invokator.Invoke(() =>
        {
            OnPropertyChanging(nameof(Count));
            OnPropertyChanging(IndexerName);

            var itemsToAdd = items.ToArray();
            foreach (var item in itemsToAdd)
            {
                var index = Items.Count;
                base.InsertItem(index, item);
                CatchItemPropertyChanging(item);
                CatchItemPropertyChanged(item);
            }

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Swaps two items in the collection.
    /// </summary>
    /// <param name="item1">The first item.</param>
    /// <param name="item2">The second item.</param>
    public virtual void Swap(T item1, T item2)
    {
        Swap(IndexOf(item1), IndexOf(item2));
    }

    /// <summary>
    ///     Swaps two items in the collection by their index.
    /// </summary>
    /// <param name="index1">The index of the first item.</param>
    /// <param name="index2">The index of the second item.</param>
    /// <exception cref="ArgumentOutOfRangeException">index1 is out of range</exception>
    /// <exception cref="ArgumentOutOfRangeException">index2 is out of range</exception>
    public virtual void Swap(int index1, int index2)
    {
        _invokator.Invoke(() =>
        {
            OnPropertyChanging(IndexerName);

            (Items[index2], Items[index1]) = (Items[index1], Items[index2]);

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, Items[index1], Items[index2], index1));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, Items[index2], Items[index1], index2));
        });
    }

    /// <summary>
    ///     Removes all items from the collection.
    /// </summary>
    protected override void ClearItems()
    {
        _invokator.Invoke(() =>
        {
            OnPropertyChanging(nameof(Count));
            OnPropertyChanging(IndexerName);

            IgnoreItemPropertyChanging();
            IgnoreItemPropertyChanged();
            base.ClearItems();

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Inserts a new item into the collection on a specific index.
    /// </summary>
    /// <param name="index">The index where to add the item.</param>
    /// <param name="item">The new item to add.</param>
    /// <exception cref="ArgumentOutOfRangeException">index is out of range.</exception>
    protected override void InsertItem(int index, T item)
    {
        _invokator.Invoke(() =>
        {
            OnPropertyChanging(nameof(Count));
            OnPropertyChanging(IndexerName);

            base.InsertItem(index, item);
            CatchItemPropertyChanging(item);
            CatchItemPropertyChanged(item);

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        });
    }

    /// <summary>
    ///     Replaces all items in the collection by the new list of items.
    /// </summary>
    /// <param name="items">The new items to keep in the collection.</param>
    /// <exception cref="ArgumentNullException">items cannot be null.</exception>
    public void Replace(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        _invokator.Invoke(() =>
        {
            OnPropertyChanging(nameof(Count));
            OnPropertyChanging(IndexerName);

            IgnoreItemPropertyChanging();
            IgnoreItemPropertyChanged();
            base.ClearItems();
            var itemsToAdd = items.ToArray();
            foreach (var item in itemsToAdd)
            {
                var index = Items.Count;
                base.InsertItem(index, item);
                CatchItemPropertyChanging(item);
                CatchItemPropertyChanged(item);
            }

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Removes the first items from the collection the condition matches.
    /// </summary>
    /// <param name="condition">The match condition.</param>
    /// <returns>True if an item got removed; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">condition cannot be null.</exception>
    public virtual bool Remove(Func<T, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);

        var itemToRemove = this.FirstOrDefault(condition);
        return itemToRemove != null && Remove(itemToRemove);
    }

    /// <summary>
    ///     Removes the last items from the collection the condition matches.
    /// </summary>
    /// <param name="condition">The match condition.</param>
    /// <returns>True if an item got removed; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">condition cannot be null.</exception>
    public virtual bool RemoveLast(Func<T, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);

        var index = this.LastIndexOf(condition);
        if (index == -1)
            return false;
        RemoveAt(index);
        return true;
    }

    /// <summary>
    ///     Removes all items from the collection the condition matches.
    /// </summary>
    /// <param name="condition">The match condition.</param>
    /// <exception cref="ArgumentNullException">condition cannot be null.</exception>
    public virtual void RemoveAll(Func<T, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);

        _invokator.Invoke(() =>
        {
            OnPropertyChanging(nameof(Count));
            OnPropertyChanging(IndexerName);

            for (var i = 0; i < Count; i++)
            {
                var item = Items[i];
                if (condition(item))
                {
                    IgnoreItemPropertyChanging(item);
                    IgnoreItemPropertyChanged(item);
                    base.RemoveItem(i--);
                }
            }

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Removes given amount of items starting at the given position.
    /// </summary>
    /// <param name="index">The position where to start removal.</param>
    /// <param name="count">The amount of items to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">index is out of range.</exception>
    /// <exception cref="ArgumentOutOfRangeException">count cannot be less than 1.</exception>
    /// <exception cref="ArgumentOutOfRangeException">count after the index got out of range.</exception>
    public virtual void RemoveRange(int index, int count)
    {
        ArgumentNullException.ThrowIfNull(count);

        _invokator.Invoke(() =>
        {
            OnPropertyChanging(nameof(Count));
            OnPropertyChanging(IndexerName);

            for (var i = 0; i < count; ++i)
            {
                var item = Items[index];
                IgnoreItemPropertyChanging(item);
                IgnoreItemPropertyChanged(item);
                base.RemoveItem(index);
            }

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Sorts the items in the collection.
    /// </summary>
    public virtual void Sort()
    {
        _invokator.Invoke(() =>
        {
            OnPropertyChanging(IndexerName);

            var items = Items.ToList();
            items.Sort();
            base.ClearItems();
            foreach (var item in items)
            {
                var index = Items.Count;
                base.InsertItem(index, item);
            }

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Sorts the collection by the given <see cref="IComparer{T}" />.
    /// </summary>
    /// <param name="comparer">The <see cref="IComparer{T}" /> to be used when sort.</param>
    /// <exception cref="ArgumentNullException">comparer cannot be null.</exception>
    public virtual void Sort(IComparer<T> comparer)
    {
        ArgumentNullException.ThrowIfNull(comparer);

        _invokator.Invoke(() =>
        {
            OnPropertyChanging(IndexerName);

            var items = Items.ToList();
            items.Sort(comparer);
            base.ClearItems();
            foreach (var item in items)
            {
                var index = Items.Count;
                base.InsertItem(index, item);
            }

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Sorts the collection by the given <see cref="Comparison{T}" />.
    /// </summary>
    /// <param name="comparison">The <see cref="Comparison{T}" /> to be used when sort.</param>
    /// <exception cref="ArgumentNullException">comparison cannot be null.</exception>
    public virtual void Sort(Comparison<T> comparison)
    {
        ArgumentNullException.ThrowIfNull(comparison);

        _invokator.Invoke(() =>
        {
            OnPropertyChanging(IndexerName);

            var items = Items.ToList();
            items.Sort(comparison);
            base.ClearItems();
            foreach (var item in items)
            {
                var index = Items.Count;
                base.InsertItem(index, item);
            }

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Sorts the elements in the collection for the given amount in the given position by a <see cref="IComparer{T}" />.
    /// </summary>
    /// <param name="index">The index where to start the sorting from.</param>
    /// <param name="count">The amount of elements to sort.</param>
    /// <param name="comparer">The <see cref="IComparer{T}" /> to be used when sort.</param>
    /// <exception cref="ArgumentOutOfRangeException">index is out of range.</exception>
    /// <exception cref="ArgumentOutOfRangeException">count after the index got out of range.</exception>
    /// <exception cref="ArgumentNullException">comparer cannot be null.</exception>
    public virtual void Sort(int index, int count, IComparer<T> comparer)
    {
        ArgumentNullException.ThrowIfNull(comparer);

        _invokator.Invoke(() =>
        {
            OnPropertyChanging(IndexerName);

            var items = Items.ToList();
            items.Sort(index, count, comparer);
            base.ClearItems();
            foreach (var item in items)
            {
                var position = Items.Count;
                base.InsertItem(position, item);
            }

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Sorts the collection by the given sorting func.
    /// </summary>
    /// <typeparam name="TKey">The key of the items in the collection.</typeparam>
    /// <param name="sorter">The sorter func to use.</param>
    /// <exception cref="ArgumentNullException">sorter cannot be null.</exception>
    public virtual void Sort<TKey>(Func<T, TKey> sorter)
    {
        ArgumentNullException.ThrowIfNull(sorter);

        _invokator.Invoke(() =>
        {
            OnPropertyChanging(IndexerName);

            var items = Items.OrderBy(sorter).ToList();
            base.ClearItems();
            foreach (var item in items)
            {
                var index = Items.Count;
                base.InsertItem(index, item);
            }

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Reverses the items in the collection.
    /// </summary>
    public virtual void Reverse()
    {
        _invokator.Invoke(() =>
        {
            OnPropertyChanging(IndexerName);

            var items = Items.Reverse().ToList();
            base.ClearItems();
            foreach (var item in items)
            {
                var index = Items.Count;
                base.InsertItem(index, item);
            }

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Removes the element from the given position in the collection.
    /// </summary>
    /// <param name="index"></param>
    /// <exception cref="ArgumentOutOfRangeException">index is out of range.</exception>
    protected override void RemoveItem(int index)
    {
        _invokator.Invoke(() =>
        {
            var removedItem = this[index];

            OnPropertyChanging(nameof(Count));
            OnPropertyChanging(IndexerName);

            IgnoreItemPropertyChanging(removedItem);
            IgnoreItemPropertyChanged(removedItem);
            base.RemoveItem(index);

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem, index));
        });
    }

    /// <summary>
    ///     Replaces the item in the collection on the given index.
    /// </summary>
    /// <param name="index">The index which item to replace.</param>
    /// <param name="item">The new item to set.</param>
    /// <exception cref="ArgumentOutOfRangeException">index is out of range.</exception>
    protected override void SetItem(int index, T item)
    {
        _invokator.Invoke(() =>
        {
            var originalItem = this[index];

            OnPropertyChanging(IndexerName);

            base.SetItem(index, item);
            IgnoreItemPropertyChanging(originalItem);
            IgnoreItemPropertyChanged(originalItem);
            CatchItemPropertyChanging(item);
            CatchItemPropertyChanged(item);

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, originalItem, item, index));
        });
    }

    /// <summary>
    ///     Moves an item in the collection from one to another position.
    /// </summary>
    /// <param name="fromIndex">The old position of the item in the collection.</param>
    /// <param name="toIndex">The new position of the item in the collection.</param>
    /// <exception cref="ArgumentOutOfRangeException">fromIndex is out of range.</exception>
    /// <exception cref="ArgumentOutOfRangeException">toIndex is out of range.</exception>
    protected virtual void MoveItem(int fromIndex, int toIndex)
    {
        _invokator.Invoke(() =>
        {
            var movedItem = this[fromIndex];

            OnPropertyChanging(IndexerName);

            base.RemoveItem(fromIndex);
            base.InsertItem(toIndex, movedItem);

            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, movedItem, toIndex, fromIndex));
        });
    }

    /// <summary>
    ///     Stops raising of <see cref="CollectionChanged" />, <see cref="PropertyChanging" /> and
    ///     <see cref="PropertyChanged" /> till the return value is disposed.
    ///     After the return object got disposed the dictionary raises <see cref="PropertyChanged" />,
    ///     <see cref="PropertyChanging" /> and <see cref="CollectionChanged" /> with
    ///     <see cref="NotifyCollectionChangedAction.Reset" />.
    /// </summary>
    /// <returns></returns>
    public IDisposable DisableNotifications()
    {
        _disableNotify = new DisableNotifications();
        _disableNotify.Disposed += OnDisableNotifyDisposed;
        return _disableNotify;
    }

    private void OnDisableNotifyDisposed(object sender, EventArgs e)
    {
        var notify = (DisableNotifications)sender;
        notify.Disposed -= OnDisableNotifyDisposed;
        _disableNotify = null;
        _invokator.Invoke(() =>
        {
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    ///     Raises <see cref="PropertyChanging" />.
    /// </summary>
    /// <param name="propertyName">The name of the property to change.</param>
    protected virtual void OnPropertyChanging(string propertyName)
    {
        if (_disableNotify == null)
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }

    /// <summary>
    ///     Raises <see cref="PropertyChanged" />.
    /// </summary>
    /// <param name="propertyName">The name of the property which is about to change.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (_disableNotify == null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Raises <see cref="CollectionChanged" />.
    /// </summary>
    /// <param name="e">The event args to raise with.</param>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (_disableNotify == null)
            CollectionChanged?.Invoke(this, e);
    }

    private void CatchItemPropertyChanging()
    {
        if (!CatchPropertyChanging)
            return;

        for (var i = 0; i < Count; ++i)
            CatchItemPropertyChanging(Items[i]);
    }

    private void CatchItemPropertyChanging(T item)
    {
        if (!CatchPropertyChanging)
            return;

        if (item is INotifyPropertyChanging notifyItem)
            notifyItem.PropertyChanging += NotifyItemPropertyChanging;
    }

    private void CatchItemPropertyChanged()
    {
        if (!CatchPropertyChanged)
            return;

        for (var i = 0; i < Count; ++i)
            CatchItemPropertyChanged(Items[i]);
    }

    private void CatchItemPropertyChanged(T item)
    {
        if (!CatchPropertyChanged)
            return;

        if (item is INotifyPropertyChanged notifyItem)
            notifyItem.PropertyChanged += NotifyItemPropertyChanged;
    }

    private void IgnoreItemPropertyChanging()
    {
        if (!CatchPropertyChanging)
            return;

        for (var i = 0; i < Count; ++i)
            IgnoreItemPropertyChanging(Items[i]);
    }

    private void IgnoreItemPropertyChanging(T item)
    {
        if (!CatchPropertyChanging)
            return;

        if (item is INotifyPropertyChanging notifyItem)
            notifyItem.PropertyChanging -= NotifyItemPropertyChanging;
    }

    private void IgnoreItemPropertyChanged()
    {
        for (var i = 0; i < Count; ++i)
            IgnoreItemPropertyChanged(Items[i]);
    }

    private void IgnoreItemPropertyChanged(T item)
    {
        if (!CatchPropertyChanged)
            return;

        if (item is INotifyPropertyChanged notifyItem)
            notifyItem.PropertyChanged -= NotifyItemPropertyChanged;
    }

    private void NotifyItemPropertyChanging(object sender, PropertyChangingEventArgs e)
    {
        if (CatchPropertyChanging)
            ItemPropertyChanging?.Invoke(sender, e);
    }

    private void NotifyItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (CatchPropertyChanged)
            ItemPropertyChanged?.Invoke(sender, e);
    }
}