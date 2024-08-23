// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectEx.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Extends the <see cref="object" /> with some helper methods.
/// </summary>
public static class ObjectEx
{
    /// <summary>
    ///     Checks if the <see cref="object" /> is null or an empty string.
    /// </summary>
    /// <param name="element">The <see cref="object" /> to check.</param>
    /// <returns>True if the <see cref="object" /> is null or an empty string; otherwise false.</returns>
    public static bool IsNullOrEmpty(this object element)
    {
        return element == null || string.IsNullOrEmpty(element.ToString());
    }

    /// <summary>
    ///     Checks if the <see cref="object" /> is null, an empty string or a string which consists of whitespace (or tabs)
    ///     only.
    /// </summary>
    /// <param name="element">The <see cref="object" /> to check.</param>
    /// <returns>True if the <see cref="object" /> is null, empty or consists only of whitespace (or tabs); otherwise false.</returns>
    public static bool IsNullOrWhiteSpace(this object element)
    {
        return element == null || string.IsNullOrWhiteSpace(element.ToString());
    }
}