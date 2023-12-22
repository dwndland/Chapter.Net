# Chapter.Net Library

## Overview
Chapter.Net brings classes and operations for every day projects. Its the perfect base for any MVVM project.

## Features
- **ObservableObject:** A base class implementing INotifyPropertyChanging and INotifyPropertyChanged.
- **ValidatableObservableObject:** A ObservableObject extended with the NotifyDataErrorInfo.
- **DelegateCommand:** An ICommand to connect a command to a delegate callback.
- **AsyncDelegateCommand:** An ICommand to connect a command to an async delegate callback.
- **AsyncEventHandler:** An async event handler.
- **EventArgs:** Event args in the style of tuples for quick and easy parameter forwarding.
- **Extensions:** Objects and collection extensions with useful features.
- **GarbageTruck:** A collector for IDisposables for an easy dispose of all.
- **NameOf:** Helper methods to stringify types including their namespaces and more.
- **ObservableList:** A custom ObservableCollection with all features from regular lists and fully boundable.
- **ServiceLocator:** A tiny helper to execute a resolve at any time if it cannot be passed on constructors.
- **Tasks:** Extensions on Tasks for start a task from a sync method with follow ups and other settings.
- **Validation:** Helper for an easy usage of INotifyDataErrorInfo.
- **WorkingIndicator:** A tiny helper object to see if an action is ongoing or already done.

## Getting Started

1. **Installation:**
    - Install the Chapter.Net library via NuGet Package Manager:
    ```bash
    dotnet add package Chapter.Net
    ```

2. **ObservableObject:**
    - Usage
    ```csharp
    public class ObservableObjectViewModel : ObservableObject
    {
        private int _value;

        public ObservableObjectViewModel()
        {
            Value = 1;
            LowerCommand = new DelegateCommand(Lower);
            HigherCommand = new DelegateCommand(Higher);
        }

        public IDelegateCommand LowerCommand { get; }

        public IDelegateCommand HigherCommand { get; }

        public int Value
        {
            get => _value;
            private set => NotifyAndSetIfChanged(ref _value, value);
        }

        private void Lower()
        {
            --Value;
        }

        private void Higher()
        {
            ++Value;
        }
    }
    ```

3. **ValidatableObservableObject:**
    - Usage
    ```csharp
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
    ```

4. **DelegateCommand:**
    - Usage
    ```csharp
    public class DelegateCommandViewModel : ObservableObject
    {
        private int _value;

        public DelegateCommandViewModel()
        {
            Value = 1;
            LowerCommand = new DelegateCommand(Lower);
            HigherCommand = new DelegateCommand(Higher);
        }

        public IDelegateCommand LowerCommand { get; }

        public IDelegateCommand HigherCommand { get; }

        public int Value
        {
            get => _value;
            private set => NotifyAndSetIfChanged(ref _value, value);
        }

        private void Lower()
        {
            --Value;
        }

        private void Higher()
        {
            ++Value;
        }
    }
    ```

5. **AsyncDelegateCommand:**
    - Usage
    ```csharp
    public class AsyncDelegateCommandViewModel : ObservableObject
    {
        private int _value;

        public AsyncDelegateCommandViewModel()
        {
            Value = 1;
            LowerCommand = new AsyncDelegateCommand(Lower);
            HigherCommand = new AsyncDelegateCommand(Higher);
        }

        public IDelegateCommand LowerCommand { get; }

        public IDelegateCommand HigherCommand { get; }

        public int Value
        {
            get => _value;
            private set => NotifyAndSetIfChanged(ref _value, value);
        }

        private async Task Lower()
        {
            await Task.Delay(1000);
            --Value;
        }

        private async Task Higher()
        {
            await Task.Delay(1000);
            ++Value;
        }
    }
    ```

6. **AsyncEventHandler:**
    - Usage
    ```csharp
    public class AsyncEventHandlerViewModel
    {
        public AsyncEventHandlerViewModel()
        {
            SendAsyncEventCommand = new AsyncDelegateCommand(SendAsyncEvent);
            EventReceiver = new EventReceiver(this);
        }

        public IDelegateCommand SendAsyncEventCommand { get; }

        public EventReceiver EventReceiver { get; }

        public event AsyncEventHandler DemoEvent;

        private async Task SendAsyncEvent()
        {
            if (DemoEvent != null)
                await DemoEvent.Invoke(this, new EventArgs<int, int>(1, 2));
        }
    }
    ```
    ```csharp
    public class EventReceiver : ObservableObject, IDisposable
    {
        private readonly AsyncEventHandlerViewModel _sender;

        public EventReceiver(AsyncEventHandlerViewModel sender1)
        {
            _sender = sender1;
            sender.DemoEvent += OnSenderDemoEvent;
        }

        private async Task OnSenderDemoEvent(object sender, EventArgs e)
        {
            await Task.Delay(1000);
        }
    }
    ```

7. **EventArgs:**
    - Usage
    ```csharp
    public class EventArgsViewModel
    {
        public EventArgsViewModel()
        {
            SendEventCommand = new DelegateCommand(SendEvent);
            EventReceiver = new EventReceiver(this);
        }

        public IDelegateCommand SendEventCommand { get; }

        public EventReceiver EventReceiver { get; }

        public event EventHandler<EventArgs<int, int>> DemoEvent;

        private void SendEvent()
        {
            DemoEvent?.Invoke(this, new EventArgs<int, int>(1, 1));
        }
    }
    ```

8. **Extensions:**
    - Usage
    ```csharp
    private void AddIf()
    {
        var input = new List<int> { 1, 15, 22, 16 };

        var target = new List<int>();
        foreach (var i in input)
            target.AddIf(i, x => x > 15);
    }
    ```
    ```csharp
    private void IsNullOrWhiteSpace()
    {
        var result = "Demo".IsNullOrWhiteSpace();
    }
    ```
    ```csharp
    private void IndexOf()
    {
        var collection = new List<int> { 1, 4, 3, 4, 1 };

        var result = collection.IndexOf(x => x > 3);
    }
    ```
    ```csharp
    private void Repeat()
    {
        var i = 1;
        var list = EnumerableEx.Repeat(() => i++, 4);
    }
    ```

9. **GarbageTruck:**
    - Usage
    ```csharp
    public class GarbageTruckViewModel : IDisposable
    {
        private readonly GarbageTruck _garbageTruck;

        public GarbageTruckViewModel()
        {
            _garbageTruck = new GarbageTruck();

            _garbageTruck.Add(new EventReceiver());
            _garbageTruck.Add(new EventReceiver());
            _garbageTruck.Add(new EventReceiver());
        }

        public void Dispose()
        {
            _garbageTruck.Dispose();
        }
    }
    ```

10. **NameOf:**
    - Usage
    ```csharp
    public class NameOfViewModel
    {
        public string Name => NameOf.Name<NameOfViewModel>();

        public string NameWithProperty => NameOf.Name<NameOfViewModel>(nameof(Name));

        public string Namespace => NameOf.Namespace<NameOfViewModel>();

        public string FullName => NameOf.FullName<NameOfViewModel>();

        public string FullNameWithProperty => NameOf.FullName<NameOfViewModel>(nameof(Name));
    }
    ```

11. **ObservableList:**
    - Usage
    ```csharp
    var coll = new ObservableList<ListItemViewModel>();
    var items = EnumerableEx.Repeat(() => new ListItemViewModel(Guid.NewGuid().ToString()), 100);
    coll.AddRange(items);
    ```
    ```csharp
    var coll = new ObservableList<ListItemViewModel>();
    coll.RemoveAll(x => x.Name.Contains("a"));
    ```
    ```csharp
    var coll = new ObservableList<ListItemViewModel>();
    coll.Sort(x => x.Name); }
    ```

12. **ServiceLocator:**
    - Usage
    ```csharp
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.UseServiceLocator();
    ```
    ```csharp
    private void DoIt()
    {
        var viewModel = ServiceLocator.Resolve<ServiceLocatorViewModel>();
    }
    ```

13. **Tasks:**
    - Usage
    ```csharp
    public class TaskExtensionsViewModel : ObservableObject
    {
        private int _value;

        public TaskExtensionsViewModel()
        {
            Value = 1;
            LowerCommand = new DelegateCommand(Lower);
            HigherCommand = new DelegateCommand(Higher);
        }

        public IDelegateCommand LowerCommand { get; }

        public IDelegateCommand HigherCommand { get; }

        public int Value
        {
            get => _value;
            private set => NotifyAndSetIfChanged(ref _value, value);
        }

        private void Lower()
        {
            Update(-2).FireAndForget();
            --Value;
        }

        private void Higher()
        {
            Update(2).FireAndForget();
            ++Value;
        }

        private async Task Update(int value)
        {
            await Task.Delay(1000);
            Value += value;
        }
    }
    ```

14. **Validation:**
    - Usage
    ```csharp
    public class ValidationViewModel : ObservableObject, INotifyDataErrorInfo
    {
        private readonly NotifyDataErrorInfo _errors = new();
        private string _validatedValue = string.Empty;

        public string ValidatedValue
        {
            get => _validatedValue;
            set
            {
                NotifyAndSetIfChanged(ref _validatedValue, value);
                _errors.Evaluate(_validatedValue == "Hello", "Wrong Word", nameof(ValidatedValue));
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.GetErrors(propertyName);
        }

        public bool HasErrors => _errors.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add => _errors.ErrorsChanged += value;
            remove => _errors.ErrorsChanged -= value;
        }
    }
    ```

15. **WorkingIndicator:**
    - Usage
    ```csharp
    public class WorkingIndicatorViewModel : ObservableObject
    {
        private WorkingIndicator _workingIndicator;

        public WorkingIndicatorViewModel()
        {
            TestCommand = new AsyncDelegateCommand(Test);
        }

        public bool IsActive => WorkingIndicator.IsActive(_workingIndicator);

        public IDelegateCommand TestCommand { get; }

        private async Task Test()
        {
            using (_workingIndicator = new WorkingIndicator())
            {
                NotifyPropertyChanged(nameof(IsActive));
                await Task.Delay(1000);
            }

            NotifyPropertyChanged(nameof(IsActive));
        }
    }
    ```

## Links
* [NuGet](https://www.nuget.org/packages/Chapter.Net)
* [GitHub](https://github.com/dwndland/Chapter.Net)

## License
Copyright (c) David Wendland. All rights reserved.
Licensed under the MIT License. See LICENSE file in the project root for full license information.
