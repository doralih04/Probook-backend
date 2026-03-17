using System;
using ProBook.Domain.Enums;

namespace ProBook.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public ReservationStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}