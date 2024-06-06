using FlightApp.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FlightApp.Domain.Entities
{
    public class Reservation : BaseEntity
    {

        public string UserId { get; set; }
        public string FlightIata { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfSeats { get; set; }
    }
}

