using ServiceProvider.Domain.Entities;

namespace ServiceProvider.Infrastructure.Interfaces;

public interface IServiceRepository
{
    Task<int> AddService(Service service);
    Task UpdateService(Service service);
    Task<Service> GetService(int serviceId);
}