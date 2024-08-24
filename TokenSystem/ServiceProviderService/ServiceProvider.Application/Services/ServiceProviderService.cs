using ServiceProvider.Application.Interfaces;
using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.Application.Services;

public class ServiceProviderService : IServiceProviderService
{
    private readonly ITokenIssuanceClient _tokenIssuanceClient;


    public ServiceProviderService(ITokenIssuanceClient tokenIssuanceClient)
    {
        _tokenIssuanceClient = tokenIssuanceClient;
    }

    public Task<IEnumerable<Token>> GetPendingTokens()
    {
        throw new NotImplementedException();
    }

    public Task UpdateStatus(int tokenId, TokenStatus status, string? description)
    {
        throw new NotImplementedException();
    }
}