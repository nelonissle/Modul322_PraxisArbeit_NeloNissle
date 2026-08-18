using RoomBook.Models;

namespace RoomBook.Services;

public class RoomBookService
{
    private readonly List<Room> _rooms;
    private readonly List<Reservation> _reservations = new();
    private int _nextReservationId = 1;

    public RoomBookService()
    {
        _rooms = new List<Room>
        {
            new Room { Id = 1, Name = "Raum Alpha", Capacity = 4, HasBeamer = false, HasVideoConference = false, HasWhiteboard = true, Description = "Kleiner Besprechungsraum für bis zu 4 Personen." },
            new Room { Id = 2, Name = "Raum Beta", Capacity = 8, HasBeamer = true, HasVideoConference = false, HasWhiteboard = true, Description = "Mittlerer Raum mit Beamer und Whiteboard." },
            new Room { Id = 3, Name = "Raum Gamma", Capacity = 12, HasBeamer = true, HasVideoConference = true, HasWhiteboard = true, Description = "Grosser Konferenzraum mit Videokonferenz." },
            new Room { Id = 4, Name = "Raum Delta", Capacity = 6, HasBeamer = false, HasVideoConference = true, HasWhiteboard = false, Description = "Videokonferenzraum für 6 Personen." },
            new Room { Id = 5, Name = "Raum Epsilon", Capacity = 10, HasBeamer = true, HasVideoConference = false, HasWhiteboard = false, Description = "Schulungsraum mit Beamer." },
            new Room { Id = 6, Name = "Raum Zeta", Capacity = 20, HasBeamer = true, HasVideoConference = true, HasWhiteboard = true, Description = "Grosser Saal für Events und Präsentationen." },
        };

        // Seed some reservations
        var today = DateOnly.FromDateTime(DateTime.Today);
        _reservations.Add(new Reservation
        {
            Id = _nextReservationId++,
            EmployeeName = "Anna Müller",
            RoomId = 2,
            Room = _rooms.First(r => r.Id == 2),
            Date = today,
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(10, 0),
            Notes = "Teambesprechung"
        });
        _reservations.Add(new Reservation
        {
            Id = _nextReservationId++,
            EmployeeName = "Thomas Keller",
            RoomId = 3,
            Room = _rooms.First(r => r.Id == 3),
            Date = today,
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(16, 0),
            Notes = "Kundenpräsentation"
        });
    }

    public IReadOnlyList<Room> GetRooms() => _rooms.AsReadOnly();

    public Room? GetRoom(int id) => _rooms.FirstOrDefault(r => r.Id == id);

    public IReadOnlyList<Reservation> GetReservations() => _reservations.AsReadOnly();

    public IReadOnlyList<Reservation> GetReservationsForRoom(int roomId) =>
        _reservations.Where(r => r.RoomId == roomId).ToList().AsReadOnly();

    public bool IsRoomAvailable(int roomId, DateOnly date, TimeOnly start, TimeOnly end, int? excludeReservationId = null)
    {
        return !_reservations.Any(r =>
            r.RoomId == roomId &&
            r.Date == date &&
            r.Id != excludeReservationId &&
            start < r.EndTime &&
            end > r.StartTime);
    }

    public (bool Success, string Message) CreateReservation(Reservation reservation)
    {
        if (reservation.EndTime <= reservation.StartTime)
            return (false, "Die Endzeit muss nach der Startzeit liegen.");

        if (!IsRoomAvailable(reservation.RoomId ?? 0, reservation.Date, reservation.StartTime, reservation.EndTime))
            return (false, "Der Raum ist in diesem Zeitfenster bereits belegt. Bitte wählen Sie ein anderes Zeitfenster.");

        var room = _rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null)
            return (false, "Raum nicht gefunden.");

        reservation.Id = _nextReservationId++;
        reservation.Room = room;
        _reservations.Add(reservation);
        return (true, $"Reservation erfolgreich erstellt! Reservations-ID: {reservation.Id}");
    }

    public (bool Success, string Message) CancelReservation(int reservationId)
    {
        var reservation = _reservations.FirstOrDefault(r => r.Id == reservationId);
        if (reservation == null)
            return (false, $"Keine Reservation mit der ID {reservationId} gefunden.");

        _reservations.Remove(reservation);
        return (true, $"Reservation #{reservationId} ({reservation.EmployeeName}, {reservation.Date:dd.MM.yyyy}) wurde erfolgreich storniert.");
    }

    public bool RoomIsOccupiedNow(int roomId)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var time = TimeOnly.FromDateTime(now);
        return _reservations.Any(r => r.RoomId == roomId && r.Date == today && r.StartTime <= time && r.EndTime > time);
    }
}
