using System.ComponentModel.DataAnnotations;

namespace RoomBook.Models;

public class Reservation
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Mitarbeitername ist erforderlich.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name muss zwischen 2 und 100 Zeichen haben.")]
    public string EmployeeName { get; set; } = "";

    [Required(ErrorMessage = "Raum ist erforderlich.")]
    public int? RoomId { get; set; }

    [Required(ErrorMessage = "Datum ist erforderlich.")]
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required(ErrorMessage = "Startzeit ist erforderlich.")]
    public TimeOnly StartTime { get; set; } = new TimeOnly(8, 0);

    [Required(ErrorMessage = "Endzeit ist erforderlich.")]
    public TimeOnly EndTime { get; set; } = new TimeOnly(9, 0);

    public string? Notes { get; set; }

    public Room? Room { get; set; }
}
