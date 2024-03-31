// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IDelegateCommand.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.Net
{
    /// <summary>
    ///     Extends the ICommand with an <see cref="RaiseCanExecuteChanged" />.
    /// </summary>
    public interface IDelegateCommand : ICommand
    {
        /// <summary>
        ///     Raises the CanExecuteChanged to have the CanExecute checked again.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}