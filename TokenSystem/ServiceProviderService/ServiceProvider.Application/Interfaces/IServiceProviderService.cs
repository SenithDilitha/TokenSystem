using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.Application.Interfaces;

public interface IServiceProviderService
{
    Task<IEnumerable<Token>> GetPendingTokens();
    Task UpdateStatus(int tokenId, TokenStatus status, string? description);
}