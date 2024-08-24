using TokenIssuanceService.Domain.Entities;

namespace TokenIssuanceService.Application.Interfaces;

public interface ITokenIssuanceService
{
    Task<Token> IssueToken(string clientName, ServiceCategory serviceCategory);
    Task<IEnumerable<Token>> GetPendingTokens();
    Task<bool> UpdateTokenStatus(int tokenId, TokenStatus status);
}