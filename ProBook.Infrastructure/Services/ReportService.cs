using System.Linq;
using ProBook.Application.Interfaces;

namespace ProBook.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly List<ProBook.Domain.Entities.Reservation> _reservations = new List<ProBook.Domain.Entities.Reservation>();
        private readonly List<ProBook.Domain.Entities.Room> _rooms = new List<ProBook.Domain.Entities.Room>
        {
            new ProBook.Domain.Entities.Room { Id = 1, Name = "Suite Deluxe", Type = ProBook.Domain.Enums.RoomType.Suite, Price = 200 },
            new ProBook.Domain.Entities.Room { Id = 2, Name = "Double Room", Type = ProBook.Domain.Enums.RoomType.Double, Price = 120 },
            new ProBook.Domain.Entities.Room { Id = 3, Name = "Individual Room", Type = ProBook.Domain.Enums.RoomType.Individual, Price = 80 },
            new ProBook.Domain.Entities.Room { Id = 4, Name = "Suite Premium", Type = ProBook.Domain.Enums.RoomType.Suite, Price = 250 },
            new ProBook.Domain.Entities.Room { Id = 5, Name = "Double Superior", Type = ProBook.Domain.Enums.RoomType.Double, Price = 150 },
            new ProBook.Domain.Entities.Room { Id = 6, Name = "Individual Economy", Type = ProBook.Domain.Enums.RoomType.Individual, Price = 60 }
        };

        public async Task<object> GetStatsAsync()
        {
            var totalRevenue = _reservations.Sum(r => r.TotalPrice);
            var totalReservations = _reservations.Count;
            var totalRooms = _rooms.Count;
            var occupancyRate = totalReservations > 0 ? (double)totalReservations / totalRooms * 100 : 0;

            return new
            {
                totalRevenue,
                occupancyRate = Math.Round(occupancyRate, 2)
            };
        }

        public async Task<object> GetDistributionAsync()
        {
            var distribution = _reservations
                .GroupBy(r => _rooms.FirstOrDefault(room => room.Id == r.RoomId)?.Type ?? ProBook.Domain.Enums.RoomType.Individual)
                .Select(g => new
                {
                    type = g.Key.ToString(),
                    count = g.Count()
                })
                .ToList();

            return distribution;
        }
    }
}