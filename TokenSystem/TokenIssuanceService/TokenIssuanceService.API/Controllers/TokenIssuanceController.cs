using Microsoft.AspNetCore.Mvc;
using TokenIssuanceService.Application.Interfaces;
using TokenIssuanceService.Domain.Entities;

namespace TokenIssuanceService.API.Controllers;

[Route("api/tokens")]
[ApiController]
public class TokenIssuanceController : ControllerBase
{
    private readonly ITokenIssuanceService _tokenIssuanceService;

    public TokenIssuanceController(ITokenIssuanceService tokenIssuanceService)
    {
        _tokenIssuanceService = tokenIssuanceService;
    }

    [HttpGet("issue-token")]
    public async Task<IActionResult> IssueToken(string clientName, ServiceCategory serviceCategory)
    {
        var token = await _tokenIssuanceService.IssueToken(clientName, serviceCategory);

        return Ok(token);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingTokens()
    {
        var tokens = await _tokenIssuanceService.GetPendingTokens();

        return Ok(tokens);
    }

    [HttpPut("update-token")]
    public async Task<IActionResult> UpdateToken(int id, TokenStatus tokenStatus)
    {
        var isSuccess = await _tokenIssuanceService.UpdateTokenStatus(id, tokenStatus);

        return Ok(isSuccess);
    }
}