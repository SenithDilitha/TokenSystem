using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.Application.Interfaces;

public interface IServiceProviderService
{
    Task<IEnumerable<Token>?> GetPendingTokens();
    Task UpdateStatus(int serviceId, TokenStatus status, string? description);
    Task<int> CreateService(int tokenId);
}