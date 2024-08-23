namespace TokenIssuanceService.Domain.Entities;

public class Token
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public ServiceCategory ServiceCategory { get; set; }
    public TokenStatus Status { get; set; } = TokenStatus.Pending;
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