using ServiceProvider.Domain.Entities;

namespace ServiceProvider.Infrastructure.Interfaces;

public interface IServiceRepository
{
    Task<int> AddService(Service service);
    Task UpdateService(int serviceId, DateTime completedTime, string description);
}