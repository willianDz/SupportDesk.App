using SupportDesk.App.Models.Requests;

namespace SupportDesk.App.Models.User;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty!;
    public string FirstName { get; set; } = string.Empty!;
    public string LastName { get; set; } = string.Empty!;
    public DateTime? BirthDate { get; set; }
    public int? GenderId { get; set; }
    public string? PhotoUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAdmin { get; set; }
    public bool IsSupervisor { get; set; }

    public Gender.Gender? Gender { get; set; }
    public ICollection<UserZone>? UserZones { get; set; }
    public ICollection<UserRequestType>? UserRequestTypes { get; set; }
    public ICollection<Request>? ReviewedRequests { get; set; }
}
