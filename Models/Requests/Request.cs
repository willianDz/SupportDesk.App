using SupportDesk.App.Models.User;
using SupportDesk.App.Models.Zone;

namespace SupportDesk.App.Models.Requests;

public class Request
{
    public int Id { get; set; }
    public Guid? ReviewerUserId { get; set; }
    public int RequestTypeId { get; set; }
    public int ZoneId { get; set; }
    public string Comments { get; set; } = string.Empty!;
    public DateTime? StartReviewDate { get; set; }
    public DateTime? ApprovalRejectionDate { get; set; }
    public int RequestStatusId { get; set; }
    public string? ReviewerUserComments { get; set; }
    public bool IsActive { get; set; } = true;

    public User.User? ReviewerUser { get; set; }
    public RequestType RequestType { get; set; } = null!;
    public Zone.Zone Zone { get; set; } = null!;
    public RequestStatus RequestStatus { get; set; } = null!;
    public ICollection<RequestDocument>? RequestDocuments { get; set; }
}
