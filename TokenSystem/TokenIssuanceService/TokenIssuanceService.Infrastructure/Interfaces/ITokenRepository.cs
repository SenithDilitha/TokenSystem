using TokenIssuanceService.Domain.Entities;

namespace TokenIssuanceService.Infrastructure.Interfaces;

public interface ITokenRepository
{
    Task<int> AddToken(Token token);
    Task<IEnumerable<Token>> GetTokenByStatus(TokenStatus status);
    Task UpdateToken(Token? token);
    Task<Token?> GetTokenById(int id);
}