namespace Domain.Models;

public class Issue : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
}

public enum Status
{
    Open,
    InProgress,
    Closed
}
