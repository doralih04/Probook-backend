using System.Linq;
using ProBook.Application.Interfaces;
using ProBook.Infrastructure.MockData;

namespace ProBook.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        public async Task<object> GetStatsAsync()
        {
            var totalRevenue = MockDataStore.Reservations.Sum(r => r.TotalPrice);
            var totalReservations = MockDataStore.Reservations.Count;
            var totalRooms = MockDataStore.Rooms.Count;
            var occupancyRate = totalReservations > 0 ? (double)totalReservations / totalRooms * 100 : 0;

            return new
            {
                totalRevenue,
                occupancyRate = Math.Round(occupancyRate, 2)
            };
        }

        public async Task<object> GetDistributionAsync()
        {
            var distribution = MockDataStore.Reservations
                .GroupBy(r => MockDataStore.Rooms.FirstOrDefault(room => room.Id == r.RoomId)?.Type ?? ProBook.Domain.Enums.RoomType.Single)
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