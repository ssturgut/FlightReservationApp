
using System.Collections.Generic;
using FlightApp.Domain.Entities;

namespace FlightApp.Application.Wrappers
{
    public class AviationApiResponse
    {
        public Pagination Pagination { get; set; }
        public List<FlightInfo> Data { get; set; }
    }

    public class Pagination
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }
}