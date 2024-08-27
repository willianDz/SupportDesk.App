namespace SupportDesk.App.Models.User;

public class UserRequestType
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int RequestTypeId { get; set; }
}
