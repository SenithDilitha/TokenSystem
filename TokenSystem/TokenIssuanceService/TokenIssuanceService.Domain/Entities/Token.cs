using System.Text.Json.Serialization;

namespace TokenIssuanceService.Domain.Entities;

public class Token
{
    [JsonPropertyName("id")]
    public int Id { get; set; }


    [JsonPropertyName("clientName")]
    public string ClientName { get; set; }
    
    [JsonPropertyName("serviceCategory")]
    public ServiceCategory ServiceCategory { get; set; }
    
    [JsonPropertyName("status")]
    public TokenStatus Status { get; set; } = TokenStatus.Pending;

    [JsonPropertyName("issueDateTime")]
    public DateTime IssueDateTime { get; set; }
}

public enum TokenStatus
{
    Pending,
    Resolving,
    Resolved,
    UnResolved,
    FurtherInfoRequested,
    Cancelled
}

public enum ServiceCategory
{
    Retirees,
    UnEmployed,
    Disabled
}