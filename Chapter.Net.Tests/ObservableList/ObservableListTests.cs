// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableListTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Tests;

public class ObservableListTests
{
    private TestableInvokator _invokator;
    private TestableListItem _item1;
    private TestableListItem _item2;
    private TestableListItem _item3;
    private List<TestableListItem> _items;
    private List<TestableListItem> _otherItems;
    private ObservableList<TestableListItem> _target;

    [SetUp]
    public void SetUp()
    {
        _item1 = new TestableListItem();
        _item2 = new TestableListItem();
        _item3 = new TestableListItem();
        _items = new List<TestableListItem> { _item1, _item2, _item3 };
        _otherItems = new List<TestableListItem> { new(), new(), new(), new() };
        _invokator = new TestableInvokator();
        _target = new ObservableList<TestableListItem>(_invokator) { _item1, _item2, _item3 };
        _target.CatchPropertyChanging = true;
        _target.CatchPropertyChanged = true;
    }

    [Test]
    public void Ctor_WithNullInvokator_ThrowsException()
    {
        Assert.That(() => new ObservableList<int>((IInvokator)null), Throws.ArgumentNullException);
    }

    [Test]
    public void Ctor_WithItems_TakesThemOver()
    {
        var items = new List<int> { 4, 2, 5 };

        var target = new ObservableList<int>(items);

        Assert.That(target, Is.EqualTo(items));
    }

    [Test]
    public void Ctor_WithItemsAndNullInvokator_ThrowsException()
    {
        var items = new List<int> { 4, 2, 5 };

        Assert.That(() => new ObservableList<int>(null, items), Throws.ArgumentNullException);
    }

    [Test]
    public void Invokator_SetWithNull_ThrowsException()
    {
        var target = new ObservableList<int>();

        Assert.That(() => target.Invokator = null, Throws.ArgumentNullException);
    }

    [Test]
    public void CatchPropertyChanging_SetToTrue_SubscribesToPropertyChanging()
    {
        _target.CatchPropertyChanging = false;
        _target.CatchPropertyChanging = true;

        TestItemPropertyChanging(_target, true);
    }

    [Test]
    public void CatchPropertyChanging_SetToFalse_UnsubscribesFromPropertyChanging()
    {
        _target.CatchPropertyChanging = true;
        _target.CatchPropertyChanging = false;

        TestItemPropertyChanging(_target, false);
    }

    [Test]
    public void CatchPropertyChanged_SetToTrue_SubscribesToPropertyChanged()
    {
        _target.CatchPropertyChanged = false;
        _target.CatchPropertyChanged = true;

        TestItemPropertyChanged(_target, true);
    }

    [Test]
    public void CatchPropertyChanged_SetToFalse_UnsubscribesFromPropertyChanged()
    {
        _target.CatchPropertyChanged = true;
        _target.CatchPropertyChanged = false;

        TestItemPropertyChanged(_target, false);
    }

    [Test]
    public void Move_FirstParameterIsNegative_ThrowsException()
    {
        Assert.That(() => _target.Move(-8, 2), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Move_FirstParameterTooHighNegative_ThrowsException()
    {
        Assert.That(() => _target.Move(8, 2), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Move_SecondParameterIsNegative_ThrowsException()
    {
        Assert.That(() => _target.Move(0, -8), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Move_SecondParameterTooHigh_ThrowsException()
    {
        Assert.That(() => _target.Move(0, 8), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Move_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Move(1, 2));
    }

    [Test]
    public void Move_Called_MovesTheItem()
    {
        var expected = new List<TestableListItem> { _target[0], _target[2], _target[1] };

        _target.Move(1, 2);

        Assert.That(_target, Is.EqualTo(expected));
    }

    [Test]
    public void Move_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.Move(1, 2));
    }

    [Test]
    public void Move_Called_RaisesCollectionChangedWithMove()
    {
        TestCollectionChanged(
            _target,
            () => _target.Move(1, 2),
            NotifyCollectionChangedAction.Move,
            new List<TestableListItem> { _item2 },
            new List<TestableListItem> { _item2 },
            1,
            2);
    }

    [Test]
    public void Move_Called_GoesOverInvokator()
    {
        _target.Move(1, 2);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Move_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Move(1, 2));
            TestPropertyChangedNot(_target, () => _target.Move(1, 2));
            TestCollectionChangedNot(_target, () => _target.Move(1, 2));
        }
    }

    [Test]
    public void AddRange_CalledWithNullItems_ThrowsException()
    {
        Assert.That(() => _target.AddRange(null), Throws.ArgumentNullException);
    }

    [Test]
    public void AddRange_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.AddRange(_items));
    }

    [Test]
    public void AddRange_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.AddRange(_items));
    }

    [Test]
    public void AddRange_Called_AddTheItems()
    {
        var expected = _target.Concat(_items).ToList();

        _target.AddRange(_items);

        Assert.That(_target, Is.EqualTo(expected));
    }

    [Test]
    public void AddRange_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.AddRange(_items));
    }

    [Test]
    public void AddRange_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.AddRange(_items));
    }

    [Test]
    public void AddRange_Called_RaisesCollectionChangedWithReset()
    {
        TestCollectionChanged(_target, () => _target.AddRange(_items), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void AddRange_Called_GoesOverInvokator()
    {
        _target.AddRange(_items);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void AddRange_Called_CatchesItemPropertyChanging()
    {
        _target.Clear();
        _target.AddRange(_items);

        TestItemPropertyChanging(_target, true);
    }

    [Test]
    public void AddRange_Called_CatchesItemPropertyChanged()
    {
        _target.Clear();
        _target.AddRange(_items);

        TestItemPropertyChanged(_target, true);
    }

    [Test]
    public void AddRange_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.AddRange(_items));
            TestPropertyChangedNot(_target, () => _target.AddRange(_items));
            TestCollectionChangedNot(_target, () => _target.AddRange(_items));
        }
    }

    [Test]
    public void SwapByItem_FirstItemIsNotInList_ThrowsException()
    {
        Assert.That(() => _target.Swap(new TestableListItem(), _target[2]), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void SwapByIndex_FirstIndexIsNegative_ThrowsException()
    {
        Assert.That(() => _target.Swap(-8, 2), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void SwapByIndex_FirstIndexIsTooHigh_ThrowsException()
    {
        Assert.That(() => _target.Swap(8, 2), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void SwapByItem_SecondItemIsNotInList_ThrowsException()
    {
        Assert.That(() => _target.Swap(_target[1], new TestableListItem()), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void SwapByIndex_SecondIndexIsNegative_ThrowsException()
    {
        Assert.That(() => _target.Swap(1, -8), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void SwapByIndex_SecondIndexIsTooHigh_ThrowsException()
    {
        Assert.That(() => _target.Swap(1, 8), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void SwapByItem_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Swap(_target[1], _target[2]));
    }

    [Test]
    public void SwapByIndex_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Swap(1, 2));
    }

    [Test]
    public void SwapByItem_Called_SwapsTheItems()
    {
        var expected = new List<TestableListItem> { _target[0], _target[2], _target[1] };

        _target.Swap(_target[1], _target[2]);

        Assert.That(_target, Is.EqualTo(expected));
    }

    [Test]
    public void SwapByIndex_Called_SwapsTheItems()
    {
        var expected = new List<TestableListItem> { _target[0], _target[2], _target[1] };

        _target.Swap(1, 2);

        Assert.That(_target, Is.EqualTo(expected));
    }

    [Test]
    public void SwapByItem_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Swap(_target[1], _target[2]));
    }

    [Test]
    public void SwapByIndex_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Swap(1, 2));
    }

    [Test]
    public void SwapByItem_Called_RaisesCollectionChangedWithReplaceFirstSecond()
    {
        TestCollectionChanged(_target,
            () => _target.Swap(_target[1], _target[2]),
            NotifyCollectionChangedAction.Replace,
            new List<TestableListItem> { _target[1] },
            new List<TestableListItem> { _target[2] },
            1,
            1);
    }

    [Test]
    public void SwapByIndex_Called_RaisesCollectionChangedWithReplaceFirstSecond()
    {
        TestCollectionChanged(
            _target,
            () => _target.Swap(1, 2),
            NotifyCollectionChangedAction.Replace,
            new List<TestableListItem> { _target[2] },
            new List<TestableListItem> { _target[1] },
            2,
            2);
    }

    [Test]
    public void SwapByItem_Called_RaisesCollectionChangedWithReplaceSecondFirst()
    {
        TestCollectionChanged(
            _target,
            () => _target.Swap(_target[1], _target[2]),
            NotifyCollectionChangedAction.Replace,
            new List<TestableListItem> { _target[1] },
            new List<TestableListItem> { _target[2] },
            1,
            1);
    }

    [Test]
    public void SwapByIndex_Called_RaisesCollectionChangedWithReplaceSecondFirst()
    {
        TestCollectionChanged(
            _target,
            () => _target.Swap(1, 2),
            NotifyCollectionChangedAction.Replace,
            new List<TestableListItem> { _target[2] },
            new List<TestableListItem> { _target[1] },
            2,
            2);
    }

    [Test]
    public void SwapByItem_Called_GoesOverInvokator()
    {
        _target.Swap(_target[1], _target[2]);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void SwapByIndex_Called_GoesOverInvokator()
    {
        _target.Swap(1, 2);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void SwapByItem_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Swap(_target[1], _target[2]));
            TestPropertyChangedNot(_target, () => _target.Swap(_target[1], _target[2]));
            TestCollectionChangedNot(_target, () => _target.Swap(_target[1], _target[2]));
        }
    }

    [Test]
    public void SwapByIndex_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Swap(1, 2));
            TestPropertyChangedNot(_target, () => _target.Swap(1, 2));
            TestCollectionChangedNot(_target, () => _target.Swap(1, 2));
        }
    }

    [Test]
    public void Clear_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.Clear());
    }

    [Test]
    public void Clear_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Clear());
    }

    [Test]
    public void Clear_Called_RemovesAllItems()
    {
        _target.Clear();

        Assert.That(_target, Is.Empty);
    }

    [Test]
    public void Clear_Called_IgnoresItemPropertyChanging()
    {
        _target.Clear();

        Assert.That(_items.All(x => x.HasPropertyChangingSubscriber), Is.False);
    }

    [Test]
    public void Clear_Called_IgnoresItemPropertyChanged()
    {
        _target.Clear();

        Assert.That(_items.All(x => x.HasPropertyChangedSubscriber), Is.False);
    }

    [Test]
    public void Clear_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.Clear());
    }

    [Test]
    public void Clear_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.Clear());
    }

    [Test]
    public void Clear_Called_RaisesCollectionChangedWithReset()
    {
        TestCollectionChanged(_target, () => _target.Clear(), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void Clear_Called_GoesOverInvokator()
    {
        _target.Clear();

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Clear_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Clear());
            TestPropertyChangedNot(_target, () => _target.Clear());
            TestCollectionChangedNot(_target, () => _target.Clear());
        }
    }

    [Test]
    public void Insert_CalledWithNegativeIndex_ThrowsException()
    {
        Assert.That(() => _target.Insert(-8, null), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Insert_CalledWithTooHighIndex_ThrowsException()
    {
        Assert.That(() => _target.Insert(8, null), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Insert_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.Insert(1, null));
    }

    [Test]
    public void Insert_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Insert(1, null));
    }

    [Test]
    public void Insert_Called_InsertsTheItems()
    {
        var expected = new List<TestableListItem> { _item1, _item2, _item1, _item3 };

        _target.Insert(2, _item1);

        Assert.That(_target, Is.EqualTo(expected));
    }

    [Test]
    public void Insert_Called_CatchesItemPropertyChanging()
    {
        _target.Insert(2, new TestableListItem());

        TestItemPropertyChanging(_target, true);
    }

    [Test]
    public void Insert_Called_CatchesItemPropertyChanged()
    {
        _target.Insert(2, new TestableListItem());

        TestItemPropertyChanged(_target, true);
    }

    [Test]
    public void Insert_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.Insert(2, _item1));
    }

    [Test]
    public void Insert_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.Insert(2, _item1));
    }

    [Test]
    public void Insert_Called_RaisesCollectionChangedWithAdd()
    {
        TestCollectionChanged(
            _target,
            () => _target.Insert(2, _item1),
            NotifyCollectionChangedAction.Add,
            null,
            new List<TestableListItem> { _item1 },
            -1,
            2);
    }

    [Test]
    public void Insert_Called_GoesOverInvokator()
    {
        _target.Insert(2, _item1);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Insert_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Insert(2, _item1));
            TestPropertyChangedNot(_target, () => _target.Insert(2, _item1));
            TestCollectionChangedNot(_target, () => _target.Insert(2, _item1));
        }
    }

    [Test]
    public void Replace_CalledWithNullItems_ThrowsException()
    {
        Assert.That(() => _target.Replace(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Replace_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.Replace(_otherItems));
    }

    [Test]
    public void Replace_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Replace(_otherItems));
    }

    [Test]
    public void Replace_Called_IgnoresItemPropertyChanging()
    {
        _target.Replace(_otherItems);

        Assert.That(_items.All(x => x.HasPropertyChangingSubscriber), Is.False);
    }

    [Test]
    public void Replace_Called_IgnoresItemPropertyChanged()
    {
        _target.Replace(_otherItems);

        Assert.That(_items.All(x => x.HasPropertyChangedSubscriber), Is.False);
    }

    [Test]
    public void Replace_Called_ReplacesAllItemsWithNew()
    {
        _target.Replace(_otherItems);

        Assert.That(_target, Is.EqualTo(_otherItems));
    }

    [Test]
    public void Replace_Called_CatchesItemPropertyChanging()
    {
        _target.Replace(_otherItems);

        TestItemPropertyChanging(_target, true);
    }

    [Test]
    public void Replace_Called_CatchesItemPropertyChanged()
    {
        _target.Replace(_otherItems);

        TestItemPropertyChanged(_target, true);
    }

    [Test]
    public void Replace_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.Replace(_otherItems));
    }

    [Test]
    public void Replace_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.Replace(_otherItems));
    }

    [Test]
    public void Replace_Called_RaisesCollectionChangedWithReset()
    {
        TestCollectionChanged(_target, () => _target.Replace(_otherItems), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void Replace_Called_GoesOverInvokator()
    {
        _target.Replace(_otherItems);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Replace_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Replace(_otherItems));
            TestPropertyChangedNot(_target, () => _target.Replace(_items));
            TestCollectionChangedNot(_target, () => _target.Replace(_otherItems));
        }
    }

    [Test]
    public void RemoveCondition_CalledWithNullCondition_ThrowsException()
    {
        Func<TestableListItem, bool> callback = null;

        Assert.That(() => _target.Remove(callback), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveCondition_CalledWithNoItemMatching_RemovesNothing()
    {
        _target.Remove(_ => false);

        Assert.That(_target, Is.EqualTo(_items));
    }

    [Test]
    public void RemoveCondition_CalledWithNoItemMatching_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Remove(_ => false));
            TestPropertyChangedNot(_target, () => _target.Remove(_ => false));
            TestCollectionChangedNot(_target, () => _target.Remove(_ => false));
        }
    }

    [Test]
    public void RemoveCondition_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.Remove(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveCondition_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Remove(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveCondition_Called_IgnoresItemPropertyChanging()
    {
        Assert.That(_item1.HasPropertyChangingSubscriber, Is.True);

        _target.Remove(x => Equals(x, _item1));

        Assert.That(_item1.HasPropertyChangingSubscriber, Is.False);
    }

    [Test]
    public void RemoveCondition_Called_IgnoresItemPropertyChanged()
    {
        Assert.That(_item1.HasPropertyChangedSubscriber, Is.True);

        _target.Remove(x => Equals(x, _item1));

        Assert.That(_item1.HasPropertyChangedSubscriber, Is.False);
    }

    [Test]
    public void RemoveCondition_Called_RemovesTheItem()
    {
        _target.Remove(x => Equals(x, _item1));

        _items.RemoveAt(0);
        Assert.That(_target, Is.EqualTo(_items));
    }

    [Test]
    public void RemoveCondition_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.Remove(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveCondition_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.Remove(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveCondition_Called_RaisesCollectionChangedWithRemove()
    {
        TestCollectionChanged(
            _target,
            () => _target.Remove(x => Equals(x, _item1)),
            NotifyCollectionChangedAction.Remove,
            new List<TestableListItem> { _item1 },
            null,
            0,
            -1);
    }

    [Test]
    public void RemoveCondition_Called_GoesOverInvokator()
    {
        _target.Remove(x => Equals(x, _item1));

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void RemoveCondition_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Remove(x => Equals(x, _item1)));
            _target.Insert(0, _item1);
            TestPropertyChangedNot(_target, () => _target.Remove(x => Equals(x, _item1)));
            _target.Insert(0, _item1);
            TestCollectionChangedNot(_target, () => _target.Remove(x => Equals(x, _item1)));
        }
    }

    [Test]
    public void RemoveItem_CalledWithUnknownItem_RemovesNothing()
    {
        TestableListItem item = null;

        _target.Remove(item);

        Assert.That(_target, Is.EqualTo(_items));
    }

    [Test]
    public void RemoveItem_CalledWithUnknownItem_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestableListItem item = null;
            TestPropertyChangingNot(_target, () => _target.Remove(item));
            TestPropertyChangedNot(_target, () => _target.Remove(item));
            TestCollectionChangedNot(_target, () => _target.Remove(item));
        }
    }

    [Test]
    public void RemoveItem_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.Remove(_item1));
    }

    [Test]
    public void RemoveItem_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.Remove(_item1));
    }

    [Test]
    public void RemoveItem_Called_IgnoresItemPropertyChanging()
    {
        Assert.That(_item1.HasPropertyChangingSubscriber, Is.True);

        _target.Remove(_item1);

        Assert.That(_item1.HasPropertyChangingSubscriber, Is.False);
    }

    [Test]
    public void RemoveItem_Called_IgnoresItemPropertyChanged()
    {
        Assert.That(_item1.HasPropertyChangedSubscriber, Is.True);

        _target.Remove(_item1);

        Assert.That(_item1.HasPropertyChangedSubscriber, Is.False);
    }

    [Test]
    public void RemoveItem_Called_RemovesTheItem()
    {
        _target.Remove(_item1);

        _items.RemoveAt(0);
        Assert.That(_target, Is.EqualTo(_items));
    }

    [Test]
    public void RemoveItem_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.Remove(_item1));
    }

    [Test]
    public void RemoveItem_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.Remove(_item1));
    }

    [Test]
    public void RemoveItem_Called_RaisesCollectionChangedWithRemove()
    {
        TestCollectionChanged(
            _target,
            () => _target.Remove(_item1),
            NotifyCollectionChangedAction.Remove,
            new List<TestableListItem> { _item1 },
            null,
            0,
            -1);
    }

    [Test]
    public void RemoveItem_Called_GoesOverInvokator()
    {
        _target.Remove(_item1);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void RemoveItem_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.Remove(_item1));
            TestPropertyChangedNot(_target, () => _target.Remove(_item2));
            TestCollectionChangedNot(_target, () => _target.Remove(_item3));
        }
    }

    [Test]
    public void RemoveLast_CalledWithNullCondition_ThrowsException()
    {
        Func<TestableListItem, bool> callback = null;

        Assert.That(() => _target.RemoveLast(callback), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveLast_CalledWithNoItemMatching_RemovesNothing()
    {
        _target.RemoveLast(_ => false);

        Assert.That(_target, Is.EqualTo(_items));
    }

    [Test]
    public void RemoveLast_CalledWithNoItemMatching_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.RemoveLast(_ => false));
            TestPropertyChangedNot(_target, () => _target.RemoveLast(_ => false));
            TestCollectionChangedNot(_target, () => _target.RemoveLast(_ => false));
        }
    }

    [Test]
    public void RemoveLast_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.RemoveLast(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveLast_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.RemoveLast(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveLast_Called_IgnoresItemPropertyChanging()
    {
        Assert.That(_item1.HasPropertyChangingSubscriber, Is.True);

        _target.RemoveLast(x => Equals(x, _item1));

        Assert.That(_item1.HasPropertyChangingSubscriber, Is.False);
    }

    [Test]
    public void RemoveLast_Called_IgnoresItemPropertyChanged()
    {
        Assert.That(_item1.HasPropertyChangedSubscriber, Is.True);

        _target.RemoveLast(x => Equals(x, _item1));

        Assert.That(_item1.HasPropertyChangedSubscriber, Is.False);
    }

    [Test]
    public void RemoveLast_Called_RemovesTheItem()
    {
        _target.Add(_item1);

        _target.RemoveLast(x => Equals(x, _item1));

        Assert.That(_target, Is.EqualTo(_items));
    }

    [Test]
    public void RemoveLast_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.RemoveLast(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveLast_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.RemoveLast(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveLast_Called_RaisesCollectionChangedWithRemove()
    {
        TestCollectionChanged(
            _target,
            () => _target.RemoveLast(x => Equals(x, _item1)),
            NotifyCollectionChangedAction.Remove,
            new List<TestableListItem> { _item1 },
            null,
            0,
            -1);
    }

    [Test]
    public void RemoveLast_Called_GoesOverInvokator()
    {
        _target.RemoveLast(x => Equals(x, _item1));

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void RemoveLast_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.RemoveLast(x => Equals(x, _item1)));
            TestPropertyChangedNot(_target, () => _target.RemoveLast(x => Equals(x, _item2)));
            TestCollectionChangedNot(_target, () => _target.RemoveLast(x => Equals(x, _item3)));
        }
    }

    [Test]
    public void RemoveAll_CalledWithNullCondition_ThrowsException()
    {
        Func<TestableListItem, bool> callback = null;

        Assert.That(() => _target.RemoveAll(callback), Throws.ArgumentNullException);
    }

    [Test]
    public void RemoveAll_CalledWithNoItemMatching_RemovesNothing()
    {
        _target.RemoveAll(_ => false);

        Assert.That(_target, Is.EqualTo(_items));
    }

    [Test]
    public void RemoveAll_CalledWithNoItemMatching_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.RemoveAll(_ => false));
            TestPropertyChangedNot(_target, () => _target.RemoveAll(_ => false));
            TestCollectionChangedNot(_target, () => _target.RemoveAll(_ => false));
        }
    }

    [Test]
    public void RemoveAll_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.RemoveAll(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveAll_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.RemoveAll(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveAll_Called_IgnoresItemPropertyChanging()
    {
        Assert.That(_item1.HasPropertyChangingSubscriber, Is.True);

        _target.RemoveAll(x => Equals(x, _item1));

        Assert.That(_item1.HasPropertyChangingSubscriber, Is.False);
    }

    [Test]
    public void RemoveAll_Called_IgnoresItemPropertyChanged()
    {
        Assert.That(_item1.HasPropertyChangedSubscriber, Is.True);

        _target.RemoveAll(x => Equals(x, _item1));

        Assert.That(_item1.HasPropertyChangedSubscriber, Is.False);
    }

    [Test]
    public void RemoveAll_Called_RemovesTheItem()
    {
        _target.RemoveAll(x => Equals(x, _item1));

        _items.RemoveAt(0);
        Assert.That(_target, Is.EqualTo(_items));
    }

    [Test]
    public void RemoveAll_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.RemoveAll(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveAll_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.RemoveAll(x => Equals(x, _item1)));
    }

    [Test]
    public void RemoveAll_Called_RaisesCollectionChangedWithRemove()
    {
        TestCollectionChanged(
            _target,
            () => _target.RemoveAll(x => Equals(x, _item1)),
            NotifyCollectionChangedAction.Reset,
            null,
            null,
            -1,
            -1);
    }

    [Test]
    public void RemoveAll_Called_GoesOverInvokator()
    {
        _target.RemoveAll(x => Equals(x, _item1));

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void RemoveAll_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.RemoveAll(x => Equals(x, _item1)));
            _target.Insert(0, _item1);
            TestPropertyChangedNot(_target, () => _target.RemoveAll(x => Equals(x, _item1)));
            _target.Insert(0, _item1);
            TestCollectionChangedNot(_target, () => _target.RemoveAll(x => Equals(x, _item1)));
        }
    }

    [Test]
    public void RemoveRange_CalledWithNegativeIndex_ThrowsException()
    {
        Assert.That(() => _target.RemoveRange(-8, 2), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void RemoveRange_CalledWithTooHighIndex_ThrowsException()
    {
        Assert.That(() => _target.RemoveRange(8, 2), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void RemoveRange_CalledWithNegativeCount_ThrowsException()
    {
        Assert.That(() => _target.RemoveRange(1, -8), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void RemoveRange_CalledWithZeroCount_ThrowsException()
    {
        Assert.That(() => _target.RemoveRange(1, 0), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void RemoveRange_CalledWithTooHighCountAfterIndex_ThrowsException()
    {
        Assert.That(() => _target.RemoveRange(2, 4), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void RemoveRange_Called_RaisesPropertyChangingWithCount()
    {
        TestPropertyChanging(_target, "Count", () => _target.RemoveRange(1, 2));
    }

    [Test]
    public void RemoveRange_Called_RaisesPropertyChangingWithIndexerName()
    {
        TestPropertyChanging(_target, "Item[]", () => _target.RemoveRange(1, 2));
    }

    [Test]
    public void RemoveRange_Called_IgnoresItemPropertyChanging()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_item2.HasPropertyChangingSubscriber, Is.True);
            Assert.That(_item3.HasPropertyChangingSubscriber, Is.True);
        });

        _target.RemoveRange(1, 2);

        Assert.Multiple(() =>
        {
            Assert.That(_item2.HasPropertyChangingSubscriber, Is.False);
            Assert.That(_item3.HasPropertyChangingSubscriber, Is.False);
        });
    }

    [Test]
    public void RemoveRange_Called_IgnoresItemPropertyChanged()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_item2.HasPropertyChangedSubscriber, Is.True);
            Assert.That(_item3.HasPropertyChangedSubscriber, Is.True);
        });

        _target.RemoveRange(1, 2);

        Assert.Multiple(() =>
        {
            Assert.That(_item2.HasPropertyChangedSubscriber, Is.False);
            Assert.That(_item3.HasPropertyChangedSubscriber, Is.False);
        });
    }

    [Test]
    public void RemoveRange_Called_RemovesTheItem()
    {
        _target.RemoveRange(1, 2);

        Assert.That(_target, Is.EqualTo(new[] { _item1 }));
    }

    [Test]
    public void RemoveRange_Called_RaisesPropertyChangedWithCount()
    {
        TestPropertyChanged(_target, "Count", () => _target.RemoveRange(1, 2));
    }

    [Test]
    public void RemoveRange_Called_RaisesPropertyChangedWithIndexerName()
    {
        TestPropertyChanged(_target, "Item[]", () => _target.RemoveRange(1, 2));
    }

    [Test]
    public void RemoveRange_Called_RaisesCollectionChangedWithRemove()
    {
        TestCollectionChanged(
            _target,
            () => _target.RemoveRange(1, 2),
            NotifyCollectionChangedAction.Reset,
            null,
            null,
            -1,
            -1);
    }

    [Test]
    public void RemoveRange_Called_GoesOverInvokator()
    {
        _target.RemoveRange(1, 2);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void RemoveRange_DisableNotificationsIsSet_RaisesNothing()
    {
        using (_target.DisableNotifications())
        {
            TestPropertyChangingNot(_target, () => _target.RemoveRange(1, 2));
            _target.Replace(_items);
            TestPropertyChangedNot(_target, () => _target.RemoveRange(1, 2));
            _target.Replace(_items);
            TestCollectionChangedNot(_target, () => _target.RemoveRange(1, 2));
        }
    }

    [Test]
    public void Sort_Called_RaisesPropertyChangingWithIndexerName()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        TestPropertyChanging(target, "Item[]", () => target.Sort());
    }

    [Test]
    public void Sort_Called_SortsTheItems()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        var expected = new List<int> { 1, 2, 3 };

        target.Sort();

        Assert.That(target, Is.EqualTo(expected));
    }

    [Test]
    public void Sort_Called_RaisesPropertyChangedWithIndexerName()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        TestPropertyChanged(target, "Item[]", () => target.Sort());
    }

    [Test]
    public void Sort_Called_RaisesCollectionChangedWithReset()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        TestCollectionChanged(target, () => target.Sort(), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void Sort_Called_GoesOverInvokator()
    {
        var target = new ObservableList<int>(_invokator) { 2, 3, 1 };

        target.Sort();

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Sort_DisableNotificationsIsSet_RaisesNothing()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        using (target.DisableNotifications())
        {
            TestPropertyChangingNot(target, () => target.Sort());
            TestPropertyChangedNot(target, () => target.Sort());
            TestCollectionChangedNot(target, () => target.Sort());
        }
    }

    [Test]
    public void Sort_CalledWithNullComparer_ThrowsException()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        Assert.That(() => target.Sort((IComparer<int>)null), Throws.ArgumentNullException);
    }

    [Test]
    public void Sort_CalledWithComparer_RaisesPropertyChangingWithIndexerName()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        TestPropertyChanging(target, "Item[]", () => target.Sort(new TestableComparer()));
    }

    [Test]
    public void Sort_CalledWithComparer_SortsTheItems()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        var expected = new List<int> { 1, 2, 3 };
        var comparer = new TestableComparer();

        target.Sort(comparer);

        Assert.Multiple(() =>
        {
            Assert.That(target, Is.EqualTo(expected));
            Assert.That(comparer.Triggered, Is.True);
        });
    }

    [Test]
    public void Sort_CalledWithComparer_RaisesPropertyChangedWithIndexerName()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        TestPropertyChanged(target, "Item[]", () => target.Sort(new TestableComparer()));
    }

    [Test]
    public void Sort_CalledWithComparer_RaisesCollectionChangedWithReset()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        TestCollectionChanged(target, () => target.Sort(new TestableComparer()), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void Sort_CalledWithComparer_GoesOverInvokator()
    {
        var target = new ObservableList<int>(_invokator) { 2, 3, 1 };

        target.Sort(new TestableComparer());

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Sort_CalledWithComparerDisableNotificationsIsSet_RaisesNothing()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        using (target.DisableNotifications())
        {
            TestPropertyChangingNot(target, () => target.Sort(new TestableComparer()));
            TestPropertyChangedNot(target, () => target.Sort(new TestableComparer()));
            TestCollectionChangedNot(target, () => target.Sort(new TestableComparer()));
        }
    }

    [Test]
    public void Sort_CalledWithNullComparison_ThrowsException()
    {
        var target = new ObservableList<int> { 2, 3, 1 };

        Assert.That(() => target.Sort((Comparison<int>)null), Throws.ArgumentNullException);
    }

    [Test]
    public void Sort_CalledWithComparison_RaisesPropertyChangingWithIndexerName()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        Comparison<int> comparison = (a, b) => a.CompareTo(b);

        TestPropertyChanging(target, "Item[]", () => target.Sort(comparison));
    }

    [Test]
    public void Sort_CalledWithComparison_SortsTheItems()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        var expected = new List<int> { 1, 2, 3 };
        var triggered = false;
        Comparison<int> comparison = (a, b) =>
        {
            triggered = true;
            return a.CompareTo(b);
        };

        target.Sort(comparison);

        Assert.Multiple(() =>
        {
            Assert.That(target, Is.EqualTo(expected));
            Assert.That(triggered, Is.True);
        });
    }

    [Test]
    public void Sort_CalledWithComparison_RaisesPropertyChangedWithIndexerName()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        Comparison<int> comparison = (a, b) => a.CompareTo(b);

        TestPropertyChanged(target, "Item[]", () => target.Sort(comparison));
    }

    [Test]
    public void Sort_CalledWithComparison_RaisesCollectionChangedWithReset()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        Comparison<int> comparison = (a, b) => a.CompareTo(b);

        TestCollectionChanged(target, () => target.Sort(comparison), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void Sort_CalledWithComparison_GoesOverInvokator()
    {
        var target = new ObservableList<int>(_invokator) { 2, 3, 1 };
        Comparison<int> comparison = (a, b) => a.CompareTo(b);

        target.Sort(comparison);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Sort_CalledWithComparisonDisableNotificationsIsSet_RaisesNothing()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        Comparison<int> comparison = (a, b) => a.CompareTo(b);

        using (target.DisableNotifications())
        {
            TestPropertyChangingNot(target, () => target.Sort(comparison));
            TestPropertyChangedNot(target, () => target.Sort(comparison));
            TestCollectionChangedNot(target, () => target.Sort(comparison));
        }
    }

    [Test]
    public void Sort_CalledWithIndexAndNullComparerIndexIsNegative_ThrowsException()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        Assert.That(() => target.Sort(-8, 4, null), Throws.ArgumentNullException);
    }

    [Test]
    public void Sort_CalledWithIndexAndNullComparerIndexTooHigh_ThrowsException()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        Assert.That(() => target.Sort(8, 4, null), Throws.ArgumentNullException);
    }

    [Test]
    public void Sort_CalledWithIndexAndNullComparerCountIsNegative_ThrowsException()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        Assert.That(() => target.Sort(1, -8, null), Throws.ArgumentNullException);
    }

    [Test]
    public void Sort_CalledWithIndexAndNullComparerCountAfterIndexIsTooHigh_ThrowsException()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        Assert.That(() => target.Sort(1, 8, null), Throws.ArgumentNullException);
    }

    [Test]
    public void Sort_CalledWithIndexAndNullComparer_ThrowsException()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        Assert.That(() => target.Sort(1, 4, null), Throws.ArgumentNullException);
    }

    [Test]
    public void Sort_CalledWithIndexAndComparer_RaisesPropertyChangingWithIndexerName()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        TestPropertyChanging(target, "Item[]", () => target.Sort(1, 4, new TestableComparer()));
    }

    [Test]
    public void Sort_CalledWithIndexAndComparer_SortsTheItems()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };
        var expected = new List<int> { 56, 1, 3, 12, 66 };
        var comparer = new TestableComparer();

        target.Sort(1, 4, comparer);

        Assert.Multiple(() =>
        {
            Assert.That(target, Is.EqualTo(expected));
            Assert.That(comparer.Triggered, Is.True);
        });
    }

    [Test]
    public void Sort_CalledWithIndexAndComparer_RaisesPropertyChangedWithIndexerName()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        TestPropertyChanged(target, "Item[]", () => target.Sort(1, 4, new TestableComparer()));
    }

    [Test]
    public void Sort_CalledWithIndexAndComparer_RaisesCollectionChangedWithReset()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        TestCollectionChanged(target, () => target.Sort(1, 4, new TestableComparer()), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void Sort_CalledWithIndexAndComparer_GoesOverInvokator()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };

        target.Sort(1, 4, new TestableComparer());

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Sort_CalledWithIndexAndComparerDisableNotificationsIsSet_RaisesNothing()
    {
        var target = new ObservableList<int> { 56, 12, 1, 66, 3 };
        var sorter = (int x) => x;

        target.Sort(1, 4, new TestableComparer());

        using (target.DisableNotifications())
        {
            TestPropertyChangingNot(target, () => target.Sort(sorter));
            TestPropertyChangedNot(target, () => target.Sort(sorter));
            TestCollectionChangedNot(target, () => target.Sort(sorter));
        }
    }

    [Test]
    public void Sort_CalledWithNullSorter_ThrowsException()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        // ReSharper disable once RedundantAssignment
        var sorter = (int x) => x;
        sorter = null;

        Assert.That(() => target.Sort(sorter), Throws.ArgumentNullException);
    }

    [Test]
    public void Sort_CalledWithSorter_RaisesPropertyChangingWithIndexerName()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        var sorter = (int x) => x;

        TestPropertyChanging(target, "Item[]", () => target.Sort(sorter));
    }

    [Test]
    public void Sort_CalledWithSorter_SortsTheItems()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        var expected = new List<int> { 1, 2, 3 };
        var triggered = false;
        var sorter = (int x) =>
        {
            triggered = true;
            return x;
        };

        target.Sort(sorter);

        Assert.Multiple(() =>
        {
            Assert.That(target, Is.EqualTo(expected));
            Assert.That(triggered, Is.True);
        });
    }

    [Test]
    public void Sort_CalledWithSorter_RaisesPropertyChangedWithIndexerName()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        var sorter = (int x) => x;

        TestPropertyChanged(target, "Item[]", () => target.Sort(sorter));
    }

    [Test]
    public void Sort_CalledWithSorter_RaisesCollectionChangedWithReset()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        var sorter = (int x) => x;

        TestCollectionChanged(target, () => target.Sort(sorter), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void Sort_CalledWithSorter_GoesOverInvokator()
    {
        var target = new ObservableList<int>(_invokator) { 2, 3, 1 };
        var sorter = (int x) => x;

        target.Sort(sorter);

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Sort_CalledWithSorterDisableNotificationsIsSet_RaisesNothing()
    {
        var target = new ObservableList<int> { 2, 3, 1 };
        var sorter = (int x) => x;

        using (target.DisableNotifications())
        {
            TestPropertyChangingNot(target, () => target.Sort(sorter));
            TestPropertyChangedNot(target, () => target.Sort(sorter));
            TestCollectionChangedNot(target, () => target.Sort(sorter));
        }
    }

    [Test]
    public void Reverse_Called_RaisesPropertyChangingWithIndexerName()
    {
        var target = new ObservableList<int> { 3, 2, 1 };

        TestPropertyChanging(target, "Item[]", () => target.Reverse());
    }

    [Test]
    public void Reverse_Called_ReversesAllItems()
    {
        var target = new ObservableList<int> { 3, 2, 1 };
        var expected = new List<int> { 1, 2, 3 };

        target.Reverse();

        Assert.That(target, Is.EqualTo(expected));
    }

    [Test]
    public void Reverse_Called_RaisesPropertyChangedWithIndexerName()
    {
        var target = new ObservableList<int> { 3, 2, 1 };

        TestPropertyChanged(target, "Item[]", () => target.Reverse());
    }

    [Test]
    public void Reverse_Called_RaisesCollectionChangedWithReset()
    {
        var target = new ObservableList<int> { 3, 2, 1 };

        TestCollectionChanged(target, () => target.Reverse(), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    [Test]
    public void Reverse_Called_GoesOverInvokator()
    {
        var target = new ObservableList<int> { 3, 2, 1 };

        target.Reverse();

        Assert.That(_invokator.Triggered, Is.True);
    }

    [Test]
    public void Reverse_DisableNotificationsIsSet_RaisesNothing()
    {
        var target = new ObservableList<int> { 3, 2, 1 };

        using (target.DisableNotifications())
        {
            TestPropertyChangingNot(target, () => target.Reverse());
            TestPropertyChangedNot(target, () => target.Reverse());
            TestCollectionChangedNot(target, () => target.Reverse());
        }
    }

    [Test]
    public void DisableNotifications_IsReleased_RaisesPropertyChangedWithCount()
    {
        var disable = _target.DisableNotifications();

        TestPropertyChanged(_target, "Count", () => disable.Dispose());
    }

    [Test]
    public void DisableNotifications_IsReleased_RaisesPropertyChangedWithIndexerName()
    {
        var disable = _target.DisableNotifications();

        TestPropertyChanged(_target, "Item[]", () => disable.Dispose());
    }

    [Test]
    public void DisableNotifications_IsReleased_RaisesCollectionChangedWithReset()
    {
        var disable = _target.DisableNotifications();

        TestCollectionChanged(_target, () => disable.Dispose(), NotifyCollectionChangedAction.Reset, null, null, -1, -1);
    }

    private void TestPropertyChanging<T>(ObservableList<T> target, string propertyName, Action action)
    {
        var triggered = false;
        target.PropertyChanging += TargetOnPropertyChanging;

        action();

        target.PropertyChanging -= TargetOnPropertyChanging;
        Assert.That(triggered, Is.True);

        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == propertyName)
                triggered = true;
        }
    }

    private void TestPropertyChangingNot<T>(ObservableList<T> target, Action action)
    {
        var triggered = false;
        target.PropertyChanging += TargetOnPropertyChanging;

        action();

        target.PropertyChanging -= TargetOnPropertyChanging;
        Assert.That(triggered, Is.False);

        return;

        void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            triggered = true;
        }
    }

    private void TestPropertyChanged<T>(ObservableList<T> target, string propertyName, Action action)
    {
        var triggered = false;
        target.PropertyChanged += TargetOnPropertyChanged;

        action();

        target.PropertyChanged -= TargetOnPropertyChanged;
        Assert.That(triggered, Is.True);

        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == propertyName)
                triggered = true;
        }
    }

    private void TestPropertyChangedNot<T>(ObservableList<T> target, Action action)
    {
        var triggered = false;
        target.PropertyChanged += TargetOnPropertyChanged;

        action();

        target.PropertyChanged -= TargetOnPropertyChanged;
        Assert.That(triggered, Is.False);

        return;

        void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            triggered = true;
        }
    }

    private void TestCollectionChanged<T>(
        ObservableList<T> target,
        Action action,
        NotifyCollectionChangedAction expectedAction,
        IList<T> oldItems,
        IList<T> newItems,
        int oldStartingIndex,
        int newStartingIndex)
    {
        var triggered = false;
        target.CollectionChanged += TargetOnCollectionChanged;

        action();

        target.CollectionChanged -= TargetOnCollectionChanged;
        Assert.That(triggered, Is.True);

        return;

        void TargetOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Equals(e.Action, expectedAction) &&
                ((e.OldItems == null && oldItems == null) || e.OldItems.Cast<T>().SequenceEqual(oldItems)) &&
                ((e.NewItems == null && newItems == null) || e.NewItems.Cast<T>().SequenceEqual(newItems)) &&
                Equals(e.OldStartingIndex, oldStartingIndex) &&
                Equals(e.NewStartingIndex, newStartingIndex))
                triggered = true;
        }
    }

    private void TestCollectionChangedNot<T>(ObservableList<T> target, Action action)
    {
        var triggered = false;
        target.CollectionChanged += TargetOnCollectionChanged;

        action();

        target.CollectionChanged -= TargetOnCollectionChanged;
        Assert.That(triggered, Is.False);

        return;

        void TargetOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            triggered = true;
        }
    }

    private void TestItemPropertyChanging(ObservableList<TestableListItem> target, bool expectation)
    {
        var triggered = 0;

        target.ItemPropertyChanging += OnPropertyChanging;
        target.ForEach(x => x.RaisePropertyChanging());
        target.ItemPropertyChanging -= OnPropertyChanging;

        Assert.That(triggered, expectation ? Is.EqualTo(target.Count) : Is.Zero);
        return;

        void OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            Assert.That(sender, Is.InstanceOf<TestableListItem>());
            ++triggered;
        }
    }

    private void TestItemPropertyChanged(ObservableList<TestableListItem> target, bool expectation)
    {
        var triggered = 0;

        target.ItemPropertyChanged += OnPropertyChanged;
        target.ForEach(x => x.RaisePropertyChanged());
        target.ItemPropertyChanged -= OnPropertyChanged;

        Assert.That(triggered, expectation ? Is.EqualTo(target.Count) : Is.Zero);
        return;

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Assert.That(sender, Is.InstanceOf<TestableListItem>());
            ++triggered;
        }
    }
}