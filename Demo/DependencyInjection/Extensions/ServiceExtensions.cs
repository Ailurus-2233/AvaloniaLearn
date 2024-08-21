using DependencyInjection.Interfaces;
using DependencyInjection.Services;
using DependencyInjection.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Extensions;

public static class ServiceExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IRepository, Repository>();
        collection.AddTransient<IBusinessService, BusinessService>();
        collection.AddTransient<MainViewModel>();
    }
}