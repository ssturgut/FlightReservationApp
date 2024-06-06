
namespace FlightApp.Application.Dtos
{
	public class FlightViewDto
	{
	
		public DateTime? FlightDate { get; set; }

		public string? FlightStatus { get; set; }

		public string? DepartureAirport { get; set; }

		public string? ArrivalAirport { get; set; }

		public string? FlightNumberIata { get; set; }

        public string? AirLine { get; set; }
    }
}

