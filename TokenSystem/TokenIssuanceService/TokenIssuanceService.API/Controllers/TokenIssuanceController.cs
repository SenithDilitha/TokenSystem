using Microsoft.AspNetCore.Mvc;
using TokenIssuanceService.Application.Interfaces;
using TokenIssuanceService.Domain.Entities;

namespace TokenIssuanceService.API.Controllers;

[Route("api/tokens")]
[ApiController]
public class TokenIssuanceController : ControllerBase
{
    private readonly ITokenIssuanceService _tokenIssuanceService;
    private readonly ILogger<TokenIssuanceController> _logger;

    public TokenIssuanceController(ITokenIssuanceService tokenIssuanceService, ILogger<TokenIssuanceController> logger)
    {
        _tokenIssuanceService = tokenIssuanceService;
        _logger = logger;
    }

    [HttpGet("issue-token")]
    public async Task<IActionResult> IssueToken(string clientName, ServiceCategory serviceCategory)
    {
        _logger.LogInformation("Issuing a token for client: {ClientName} in service category: {ServiceCategory}", clientName, serviceCategory);

        var token = await _tokenIssuanceService.IssueToken(clientName, serviceCategory);

        if (token != null)
        {
            _logger.LogInformation("Token issued successfully: {TokenId}", token.Id);
        }

        return Ok(token);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingTokens()
    {
        _logger.LogInformation("Fetching pending tokens.");

        var tokens = await _tokenIssuanceService.GetPendingTokens();

        _logger.LogInformation("Pending tokens fetched successfully. Count: {TokenCount}", tokens?.Count() ?? 0);

        return Ok(tokens);
    }

    [HttpPut("update-token")]
    public async Task<IActionResult> UpdateToken(int id, TokenStatus tokenStatus)
    {
        _logger.LogInformation("Updating token status for Token ID: {TokenId} to {TokenStatus}", id, tokenStatus);

        var isSuccess = await _tokenIssuanceService.UpdateTokenStatus(id, tokenStatus);

        if (isSuccess)
        {
            _logger.LogInformation("Token status updated successfully for Token ID: {TokenId}", id);
        }
        else
        {
            _logger.LogWarning("Failed to update token status for Token ID: {TokenId}", id);
        }

        return Ok(isSuccess);
    }
}