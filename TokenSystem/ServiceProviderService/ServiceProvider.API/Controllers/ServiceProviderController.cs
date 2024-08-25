using Microsoft.AspNetCore.Mvc;
using ServiceProvider.Application.Interfaces;
using TokenIssuanceService.Domain.Entities;

namespace ServiceProvider.API.Controllers;

[Route("api/service")]
[ApiController]
public class ServiceProviderController : ControllerBase
{
    private readonly IServiceProviderService _serviceProviderService;
    private readonly ILogger<ServiceProviderController> _logger;

    public ServiceProviderController(IServiceProviderService serviceProviderService,
        ILogger<ServiceProviderController> logger)
    {
        _serviceProviderService = serviceProviderService;
        _logger = logger;
    }

    [HttpGet("pending-tokens")]
    public async Task<IActionResult> GetPendingTokens()
    {
        _logger.LogInformation("Fetching pending tokens.");
        try
        {
            var pendingTokens = await _serviceProviderService.GetPendingTokens();
            _logger.LogInformation("Successfully fetched {Count} pending tokens.", pendingTokens.Count());

            return Ok(pendingTokens);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching pending tokens.");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateServiceStatus(int serviceId, TokenStatus status, string? description)
    {
        _logger.LogInformation("Updating service status. ServiceId: {ServiceId}, Status: {Status}", serviceId, status);

        try
        {
            await _serviceProviderService.UpdateStatus(serviceId, status, description);
            _logger.LogInformation("Service status updated successfully. ServiceId: {ServiceId}", serviceId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating service status. ServiceId: {ServiceId}", serviceId);
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateService(int tokenId)
    {
        _logger.LogInformation("Creating service for TokenId: {TokenId}", tokenId);

        try
        {
            var serviceId = await _serviceProviderService.CreateService(tokenId);
            _logger.LogInformation("Service created successfully. ServiceId: {ServiceId}", serviceId);

            return Ok(serviceId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating service for TokenId: {TokenId}", tokenId);
            return StatusCode(500, "Internal server error.");
        }
    }
}