using Microsoft.AspNetCore.Mvc;
using ServiceProvider.Application.Interfaces;
using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.API.Controllers;

[Route("api/service")]
[ApiController]
public class ServiceProviderController : ControllerBase
{
    private readonly IServiceProviderService _serviceProviderService;

    public ServiceProviderController(IServiceProviderService serviceProviderService)
    {
        _serviceProviderService = serviceProviderService;
    }

    [HttpGet("pending-tokens")]
    public async Task<IActionResult> GetPendingTokens()
    {
        var pendingTokens = await _serviceProviderService.GetPendingTokens();

        return Ok(pendingTokens);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateServiceStatus(int serviceId, TokenStatus status, string? description)
    {
        await _serviceProviderService.UpdateStatus(serviceId, status, description);

        return Ok();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateService(int tokenId)
    {
        var serviceId = await _serviceProviderService.CreateService(tokenId);

        return Ok(serviceId);
    }
}