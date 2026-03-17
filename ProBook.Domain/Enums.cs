namespace ProBook.Domain.Enums
{
    public enum UserRole
    {
        Manager,
        Guest
    }

    public enum RoomType
    {
        Single,
        Double,
        Suite
    }

    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
}