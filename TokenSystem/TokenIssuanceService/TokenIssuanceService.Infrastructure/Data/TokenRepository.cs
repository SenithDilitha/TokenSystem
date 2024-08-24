using Microsoft.EntityFrameworkCore;
using TokenIssuanceService.Domain.Entities;
using TokenIssuanceService.Infrastructure.Interfaces;

namespace TokenIssuanceService.Infrastructure.Data;

public class TokenRepository : ITokenRepository
{
    private readonly TokenDbContext _context;

    public TokenRepository(TokenDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddToken(Token token)
    {
        var tokenToBeSaved = _context.Add(token);
        await _context.SaveChangesAsync();

        return tokenToBeSaved.Entity.Id;
    }

    public async Task<IEnumerable<Token>> GetTokenByStatus(TokenStatus status)
    {
        return (await _context.Tokens
            .Where(t => t.Status == status)
            .ToListAsync())!;
    }

    public async Task UpdateToken(Token? token)
    {
        _context.Tokens.Update(token);
        await _context.SaveChangesAsync();
    }

    public async Task<Token?> GetTokenById(int id)
    {
        return await _context.Tokens.FindAsync(id);
    }
}