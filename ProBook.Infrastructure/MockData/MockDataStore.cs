using ProBook.Domain.Entities;
using ProBook.Domain.Enums;

namespace ProBook.Infrastructure.MockData
{
    public static class MockDataStore
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User { Id = 1, Name = "Admin", Email = "admin@probook.com", Role = UserRole.Manager, HasReserved = false },
            new User { Id = 2, Name = "Regular User", Email = "user@probook.com", Role = UserRole.Guest, HasReserved = true }
        };

        public static List<Room> Rooms { get; } = new List<Room>
        {
            new Room { Id = 1, Name = "Suite Deluxe", Type = RoomType.Suite, Price = 200, Description = "Luxury suite with ocean view", ImageUrl = "https://images.unsplash.com/photo-1631049307264-da0ec9d70304?w=400" },
            new Room { Id = 2, Name = "Double Room", Type = RoomType.Double, Price = 120, Description = "Comfortable room for two", ImageUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?w=400" },
            new Room { Id = 3, Name = "Single Room", Type = RoomType.Single, Price = 80, Description = "Cozy room for one", ImageUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427?w=400" },
            new Room { Id = 4, Name = "Suite Premium", Type = RoomType.Suite, Price = 250, Description = "Premium suite with balcony", ImageUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=400" },
            new Room { Id = 5, Name = "Double Superior", Type = RoomType.Double, Price = 150, Description = "Superior double room", ImageUrl = "https://images.unsplash.com/photo-1566665797739-1674de7a421a?w=400" },
            new Room { Id = 6, Name = "Single Economy", Type = RoomType.Single, Price = 60, Description = "Budget room for one", ImageUrl = "https://images.unsplash.com/photo-1598300042247-d088f8ab3a91?w=400" }
        };

        public static List<Reservation> Reservations { get; } = new List<Reservation>
        {
            new Reservation
            {
                Id = 1,
                UserId = 2,
                RoomId = 1,
                CheckIn = new DateTime(2026, 3, 15),
                CheckOut = new DateTime(2026, 3, 18),
                Status = ReservationStatus.Confirmed,
                TotalPrice = 600
            }
        };
    }
}