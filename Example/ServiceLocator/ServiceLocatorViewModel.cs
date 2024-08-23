// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocatorViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class ServiceLocatorViewModel
{
    private void DoIt()
    {
        var viewModel = ServiceLocator.Resolve<ServiceLocatorViewModel>();
    }
}