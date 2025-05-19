namespace LibPoint.Domain.Models.Reservations
{
    public class ReserveSeatWithTransactionModel
    {
        public Guid AppUserId { get; set; }
        public Guid SeatId { get; set; }
        public int Session { get; set; }
        public int Duration { get; set; }
    }
}
