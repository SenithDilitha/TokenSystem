using ServiceProvider.Domain.Entities;
using ServiceProvider.Infrastructure.Interfaces;

namespace ServiceProvider.Infrastructure.Data;

public class ServiceRepository : IServiceRepository
{
    private readonly ServiceDbContext _serviceDbContext;

    public ServiceRepository(ServiceDbContext serviceDbContext)
    {
        _serviceDbContext = serviceDbContext;
    }

    public async Task<int> AddService(Service service)
    {
        var serviceToAdd = _serviceDbContext.Services.Add(service);

        await _serviceDbContext.SaveChangesAsync();

        return serviceToAdd.Entity.Id;
    }

    public async Task UpdateService(Service service)
    {
        _serviceDbContext.Services.Update(service);

        await _serviceDbContext.SaveChangesAsync();
    }

    public async Task<Service> GetService(int serviceId)
    {
        return (await _serviceDbContext.Services.FindAsync(serviceId))!;
    }
}