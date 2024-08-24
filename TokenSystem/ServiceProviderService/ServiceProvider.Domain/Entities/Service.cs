namespace ServiceProvider.Domain.Entities;

public class Service
{
    public int Id { get; set; }
    public int TokenId { get; set; }
    public DateTime StartedTime { get; set; }
    public DateTime LastUpdatedTime { get; set; }
    public string? Description { get; set; }
}