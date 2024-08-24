using TokenIssuanceService.Application.Interfaces;
using TokenIssuanceService.Domain.Entities;
using TokenIssuanceService.Infrastructure.Interfaces;

namespace TokenIssuanceService.Application.Services;

public class TokenIssuanceService : ITokenIssuanceService
{
    private readonly ITokenRepository _tokenRepository;

    public TokenIssuanceService(ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public async Task<Token> IssueToken(string clientName, ServiceCategory serviceCategory)
    {
        var token = new Token
        {
            ClientName = clientName,
            ServiceCategory = serviceCategory,
            IssueDateTime = DateTime.UtcNow
        };

        var tokenId = await _tokenRepository.AddToken(token);
        token.Id = tokenId;

        return token;
    }

    public async Task<IEnumerable<Token>> GetPendingTokens()
    {
        var tokens = await _tokenRepository.GetTokenByStatus(TokenStatus.Pending);

        return tokens;
    }

    public async Task<bool> UpdateTokenStatus(int tokenId, TokenStatus status)
    {
        var token = await _tokenRepository.GetTokenById(tokenId);
        if (token == null) return false;
        token.Status = status;

        await _tokenRepository.UpdateToken(token);

        return true;
    }
}