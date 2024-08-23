// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocator.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net;

/// <summary>
///     Helper class to keep the IServiceProvider for a manual resolve of objects.
/// </summary>
public static class ServiceLocator
{
    private static IServiceProvider _serviceProvider;

    /// <summary>
    ///     Registers the service provider to use on object resolve.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use on object resolve.</param>
    /// <exception cref="ArgumentNullException">serviceProvider cannot be null.</exception>
    public static void UseServiceLocator(this IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <summary>
    ///     Registers the service provider to use on object resolve.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use on object resolve.</param>
    /// <exception cref="ArgumentNullException">serviceProvider cannot be null.</exception>
    public static void Register(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <summary>
    ///     Resolves the object by the given type.
    /// </summary>
    /// <typeparam name="T">The object type to resolve.</typeparam>
    /// <returns>The resolved object type.</returns>
    /// <exception cref="NullReferenceException">
    ///     The service provider is not set. You have to use
    ///     <see cref="UseServiceLocator" /> or the <see cref="Register" /> to set it.
    /// </exception>
    public static T Resolve<T>() where T : class
    {
        if (_serviceProvider == null)
            throw new NullReferenceException("The service provider is not set. You have to use UseServiceLocator or the Register to set it.");

        return (T)_serviceProvider.GetService(typeof(T));
    }
}