using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.Application.Interfaces;

public interface ITokenIssuanceClient
{
    Task<IEnumerable<Token>> GetPendingTokens();
    Task UpdateToken(int tokenId, TokenStatus status);
}