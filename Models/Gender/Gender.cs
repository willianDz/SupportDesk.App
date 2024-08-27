namespace SupportDesk.App.Models.Gender;

public class Gender
{
    public int GenderId { get; set; }
    public string Description { get; set; } = string.Empty!;
    public string Abbreviation { get; set; } = string.Empty!;
}
