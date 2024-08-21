using DependencyInjection.Interfaces;

namespace DependencyInjection.ViewModels;

public partial  class MainViewModel
{
    private readonly IBusinessService _businessService;

    public MainViewModel(IBusinessService businessService)
    {
        _businessService = businessService;
    }
}