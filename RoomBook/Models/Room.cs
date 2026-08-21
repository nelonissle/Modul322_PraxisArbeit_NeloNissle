namespace RoomBook.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Capacity { get; set; }
    public bool HasBeamer { get; set; }
    public bool HasVideoConference { get; set; }
    public bool HasWhiteboard { get; set; }
    public string Description { get; set; } = "";
}
