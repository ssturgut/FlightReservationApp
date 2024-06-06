
namespace FlightApp.Application.Dtos
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FlightIata { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
