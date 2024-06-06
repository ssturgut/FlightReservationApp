
using System;

namespace FlightApp.Domain.Entities
{
    public class FlightInfo
    {
        public string FlightDate { get; set; }
        public string FlightStatus { get; set; }
        public Departure Departure { get; set; }
        public Arrival Arrival { get; set; }
        public Airline Airline { get; set; }
        public Flight Flight { get; set; }
        public object Aircraft { get; set; }
        public object Live { get; set; }
    }

    public class Departure
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public string Terminal { get; set; }
        public string Gate { get; set; }
        public int? Delay { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? Estimated { get; set; }
        public DateTime? Actual { get; set; }
        public DateTime? EstimatedRunway { get; set; }
        public DateTime? ActualRunway { get; set; }
    }

    public class Arrival
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public string Terminal { get; set; }
        public string Gate { get; set; }
        public string Baggage { get; set; }
        public int? Delay { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? Estimated { get; set; }
        public DateTime? Actual { get; set; }
        public DateTime? EstimatedRunway { get; set; }
        public DateTime? ActualRunway { get; set; }
    }

    public class Airline
    {
        public string Name { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
    }

    public class Flight
    {
        public string Number { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public Codeshared Codeshared { get; set; }
    }

    public class Codeshared
    {
        public string AirlineName { get; set; }
        public string AirlineIata { get; set; }
        public string AirlineIcao { get; set; }
        public string FlightNumber { get; set; }
        public string FlightIata { get; set; }
        public string FlightIcao { get; set; }
    }
}