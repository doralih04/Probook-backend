using System;
using ProBook.Domain.Enums;

namespace ProBook.Application.DTOs
{
    public class ReservationDto
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