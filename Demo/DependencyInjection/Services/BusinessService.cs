using DependencyInjection.Interfaces;

namespace DependencyInjection.Services;

public class BusinessService : IBusinessService
{
    private readonly IRepository _repository;

    public BusinessService(IRepository repository)
    {
        _repository = repository;
    }
}