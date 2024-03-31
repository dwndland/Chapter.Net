// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WorkingIndicator.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net
{
    /// <summary>
    ///     Indicates that another method is still executing.
    ///     The work is active till the working indicator is disposed.
    /// </summary>
    public class WorkingIndicator : IDisposable
    {
        private bool _flag;

        /// <summary>
        ///     Creates a new instance of <see cref="WorkingIndicator" />.
        /// </summary>
        public WorkingIndicator()
        {
            _flag = true;
        }

        /// <summary>
        ///     Releases the current working indicator.
        /// </summary>
        public void Dispose()
        {
            _flag = false;
        }

        /// <summary>
        ///     Checks if the work is still ongoing.
        /// </summary>
        /// <param name="indicator">The working indicator to check.</param>
        /// <returns>True if the work is still ongoing; otherwise false.</returns>
        public static bool IsActive(WorkingIndicator indicator)
        {
            return indicator?._flag == true;
        }
    }
}