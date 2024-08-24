using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.Application.Interfaces;

public interface ITokenIssuanceClient
{
    Task<IEnumerable<Token>?> GetPendingTokens();
    Task<bool> UpdateToken(int tokenId, TokenStatus status);
}