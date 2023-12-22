// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocatorViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;

// ReSharper disable once CheckNamespace

namespace Example;

public class ServiceLocatorViewModel
{
    public ServiceLocatorViewModel()
    {
        //var builder = WebApplication.CreateBuilder(args);
        
        //builder.Services.UseServiceLocator();
    }

    private void DoIt()
    {
        var viewModel = ServiceLocator.Resolve<ServiceLocatorViewModel>();
    }
}