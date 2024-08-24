using ServiceProvider.Application.Interfaces;
using ServiceProvider.Domain.Entities;
using ServiceProvider.Infrastructure.Interfaces;
using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.Application.Services;

public class ServiceProviderService : IServiceProviderService
{
    private readonly ITokenIssuanceClient _tokenIssuanceClient;
    private readonly IServiceRepository _serviceRepository;


    public ServiceProviderService(ITokenIssuanceClient tokenIssuanceClient, IServiceRepository serviceRepository)
    {
        _tokenIssuanceClient = tokenIssuanceClient;
        _serviceRepository = serviceRepository;
    }

    public async Task<IEnumerable<Token>?> GetPendingTokens()
    {
        return await _tokenIssuanceClient.GetPendingTokens();
    }

    public async Task UpdateStatus(int serviceId, TokenStatus status, string? description)
    {
        var service = await _serviceRepository.GetService(serviceId);

        await _tokenIssuanceClient.UpdateToken(service.TokenId, status);

        service.LastUpdatedTime = DateTime.UtcNow;
        service.Description = description;

        await _serviceRepository.UpdateService(service);
    }

    public async Task<int> CreateService(int tokenId)
    { 
        var isTokenUpdated = await _tokenIssuanceClient.UpdateToken(tokenId, TokenStatus.Resolving);

        if (!isTokenUpdated) throw new ArgumentException("Invalid TokenID");

        var service = new Service
        {
            TokenId = tokenId,
            StartedTime = DateTime.UtcNow,
            LastUpdatedTime = DateTime.UtcNow
        };

        var serviceId = await _serviceRepository.AddService(service);

        return serviceId;
    }
}