// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableListViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class ObservableListViewModel
{
    public ObservableListViewModel()
    {
        Items = new ObservableList<ListItemViewModel>();

        AddRangeCommand = new DelegateCommand(AddRange);
        ClearCommand = new DelegateCommand(Clear);
        ReplaceCommand = new DelegateCommand(Replace);
        ReverseCommand = new DelegateCommand(Reverse);
        SortCommand = new DelegateCommand(Sort);
        MoveCommand = new DelegateCommand(Move);
        SwapCommand = new DelegateCommand(Swap);
        InsertCommand = new DelegateCommand(Insert);
        RemoveCommand = new DelegateCommand(Remove);
        RemoveLastCommand = new DelegateCommand(RemoveLast);
        RemoveAllCommand = new DelegateCommand(RemoveAll);
        RemoveRangeCommand = new DelegateCommand(RemoveRange);
    }

    public bool DisableNotification { get; set; }

    public bool CatchPropertyChanging
    {
        get => Items.CatchPropertyChanging;
        set => Items.CatchPropertyChanging = value;
    }

    public bool CatchPropertyChanged
    {
        get => Items.CatchPropertyChanged;
        set => Items.CatchPropertyChanged = value;
    }

    public IDelegateCommand AddRangeCommand { get; }
    public IDelegateCommand ClearCommand { get; }
    public IDelegateCommand ReplaceCommand { get; }
    public IDelegateCommand ReverseCommand { get; }
    public IDelegateCommand SortCommand { get; }
    public IDelegateCommand MoveCommand { get; }
    public IDelegateCommand SwapCommand { get; }
    public IDelegateCommand InsertCommand { get; }
    public IDelegateCommand RemoveCommand { get; }
    public IDelegateCommand RemoveLastCommand { get; }
    public IDelegateCommand RemoveAllCommand { get; }
    public IDelegateCommand RemoveRangeCommand { get; }

    public ObservableList<ListItemViewModel> Items { get; }

    private void AddRange()
    {
        Execute(() =>
        {
            var items = EnumerableEx.Repeat(() => new ListItemViewModel(Guid.NewGuid().ToString()), 100);
            Items.AddRange(items);
        });
    }

    private void Clear()
    {
        Execute(() => { Items.Clear(); });
    }

    private void Replace()
    {
        Execute(() =>
        {
            var items = EnumerableEx.Repeat(() => new ListItemViewModel(Guid.NewGuid().ToString()), 100);
            Items.Replace(items);
        });
    }

    private void Reverse()
    {
        Execute(() => { Items.Reverse(); });
    }

    private void Sort()
    {
        Execute(() => { Items.Sort(x => x.Name); });
    }

    private void Move()
    {
        Execute(() => { Items.Move(0, 4); });
    }

    private void Swap()
    {
        Execute(() => { Items.Swap(0, 1); });
    }

    private void Insert()
    {
        Execute(() => { Items.Insert(0, new ListItemViewModel(Guid.NewGuid().ToString())); });
    }

    private void Remove()
    {
        Execute(() => { Items.Remove(x => x.Name.Contains("a")); });
    }

    private void RemoveLast()
    {
        Execute(() => { Items.RemoveLast(x => x.Name.Contains("a")); });
    }

    private void RemoveAll()
    {
        Execute(() => { Items.RemoveAll(x => x.Name.Contains("a")); });
    }

    private void RemoveRange()
    {
        Execute(() => { Items.RemoveRange(5, 5); });
    }

    private void Execute(Action action)
    {
        IDisposable disposable = null;
        if (DisableNotification)
            disposable = Items.DisableNotifications();

        action();

        disposable?.Dispose();
    }
}