using System;
namespace FlightApp.Application.ViewModels
{
	public class BookingViewModel
	{
        public string TripType { get; set; }
        public string FlyingFrom { get; set; }
        public string FlyingTo { get; set; }
        public DateTime Departing { get; set; }
        public DateTime? Returning { get; set; }
        public int Adults { get; set; } = 1;
        public int Children { get; set; } = 0;
        public string TravelClass { get; set; }
    }
}

