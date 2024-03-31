// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NameOf.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net
{
    /// <summary>
    ///     Provides methods for an easy work with namespace and type names in combination with nameof.
    /// </summary>
    public static class NameOf
    {
        /// <summary>
        ///     Returns the name of the given type. Including the property name if given.
        ///     E.g. "MainViewModel" or "MainViewModel.Name"
        /// </summary>
        /// <typeparam name="T">The type which name to read.</typeparam>
        /// <param name="propertyName">The optional property name to append.</param>
        /// <returns>The name of the given type. Including the property name if given.</returns>
        public static string Name<T>(string propertyName = null)
        {
            return Name(typeof(T), propertyName);
        }

        /// <summary>
        ///     Returns the name of the given type. Including the property name if given.
        ///     E.g. "MainViewModel" or "MainViewModel.Name"
        /// </summary>
        /// <param name="type">The type which name to read.</param>
        /// <param name="propertyName">The optional property name to append.</param>
        /// <returns>The name of the given type. Including the property name if given.</returns>
        /// <exception cref="ArgumentNullException">type cannot be null.</exception>
        public static string Name(Type type, string propertyName = null)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (!string.IsNullOrWhiteSpace(propertyName))
                return type.Name + "." + propertyName;
            return type.Name;
        }

        /// <summary>
        ///     Returns the namespace of the given type without the type name itself.
        ///     E.g. "Application.ViewModels"
        /// </summary>
        /// <typeparam name="T">The type which namespace to read.</typeparam>
        /// <returns>The namespace of the given type without the type name itself.</returns>
        public static string Namespace<T>()
        {
            return Namespace(typeof(T));
        }

        /// <summary>
        ///     Returns the namespace of the given type without the type name itself.
        ///     E.g. "Application.ViewModels"
        /// </summary>
        /// <param name="type">The type which namespace to read.</param>
        /// <returns>The namespace of the given type without the type name itself.</returns>
        /// <exception cref="ArgumentNullException">type cannot be null.</exception>
        public static string Namespace(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.Namespace;
        }

        /// <summary>
        ///     Returns the relative path of one type namespace to the other.
        ///     E.g. "Application.ViewModels" + "Application.ViewModels.Pages" = "Pages"
        ///     If both types are in the same namespace or have no namespace in common, the result will be empty.
        /// </summary>
        /// <remarks>The order does not matter.</remarks>
        /// <typeparam name="T1">The first type which namespace to read.</typeparam>
        /// <typeparam name="T2">The second type which namespace to read.</typeparam>
        /// <returns>The relative path of one type namespace to the other.</returns>
        public static string Namespace<T1, T2>()
        {
            return Namespace(typeof(T1), typeof(T2));
        }

        /// <summary>
        ///     Returns the relative path of one type namespace to the other.
        ///     E.g. "Application.ViewModels" + "Application.ViewModels.Pages" = "Pages"
        ///     If both types are in the same namespace or have no namespace in common, the result will be empty.
        /// </summary>
        /// <remarks>The order does not matter.</remarks>
        /// <param name="type1">The first type which namespace to read.</param>
        /// <param name="type2">The second type which namespace to read.</param>
        /// <returns>The relative path of one type namespace to the other.</returns>
        /// <exception cref="ArgumentNullException">type1 cannot be null.</exception>
        /// <exception cref="ArgumentNullException">type2 cannot be null.</exception>
        public static string Namespace(Type type1, Type type2)
        {
            if (type1 == null)
                throw new ArgumentNullException(nameof(type1));
            if (type2 == null)
                throw new ArgumentNullException(nameof(type2));

            var first = type1.Namespace;
            var second = type2.Namespace;

            if (first == null || second == null)
                return string.Empty;
            if (first == second)
                return string.Empty;
#if NETCOREAPP3_0_OR_GREATER
            if (first.StartsWith(second))
                return first[(second.Length + 1)..];
            return second.StartsWith(first) ? second[(first.Length + 1)..] : string.Empty;
#else
            if (first.StartsWith(second))
                return first.Substring((second.Length + 1));
            return second.StartsWith(first) ? second.Substring(first.Length + 1) : string.Empty;
#endif
        }

        /// <summary>
        ///     Returns the namespace and name of the given type. Including the property name if given.
        ///     E.g. "Application.ViewModels.MainViewModel" or "Application.ViewModels.MainViewModel.Name"
        /// </summary>
        /// <typeparam name="T">The type which namespace and name to read.</typeparam>
        /// <param name="propertyName">The optional property name to append.</param>
        /// <returns>The namespace and name of the given type. Including the property name if given.</returns>
        public static string FullName<T>(string propertyName = null)
        {
            return FullName(typeof(T), propertyName);
        }

        /// <summary>
        ///     Returns the namespace and name of the given type. Including the property name if given.
        ///     E.g. "Application.ViewModels.MainViewModel" or "Application.ViewModels.MainViewModel.Name"
        /// </summary>
        /// <param name="type">The type which namespace and name to read.</param>
        /// <param name="propertyName">The optional property name to append.</param>
        /// <returns>The namespace and name of the given type. Including the property name if given.</returns>
        /// <exception cref="ArgumentNullException">type cannot be null.</exception>
        public static string FullName(Type type, string propertyName = null)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (!string.IsNullOrWhiteSpace(propertyName))
                return type.FullName + "." + propertyName;
            return type.FullName;
        }
    }
}