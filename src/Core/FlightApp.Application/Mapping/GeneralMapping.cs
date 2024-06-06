using System;
using AutoMapper;
using FlightApp.Application.Dtos;
using FlightApp.Application.Features.Commands;
using FlightApp.Application.Features.Queries.GetProductById;
using FlightApp.Application.Wrappers;
using FlightApp.Domain.Entities;

namespace FlightApp.Application.Mapping
{
	public class GeneralMapping : Profile
	{

		public GeneralMapping()
		{
			CreateMap<Product, ProductViewDto>().ReverseMap();

            CreateMap<Product, CreateProductCommand>().ReverseMap();

            CreateMap<Product, GetProductByIdViewModel>().ReverseMap();

            Random rnd = new Random();
            CreateMap<FlightInfo, FlightViewDto>()
                .ForMember(dest => dest.DepartureAirport, opt => opt.MapFrom(src => src.Departure != null ? src.Departure.Airport : string.Empty))
                .ForMember(dest => dest.ArrivalAirport, opt => opt.MapFrom(src => src.Arrival != null ? src.Arrival.Airport : string.Empty))
                .ForMember(dest => dest.FlightDate, opt => opt.MapFrom(src => src.FlightDate != null ? DateTime.Parse(src.FlightDate) : DateTime.Today.AddDays(rnd.Next(1, 100))))
                .ForMember(dest => dest.FlightStatus, opt => opt.MapFrom(src => src.FlightStatus ?? string.Empty))
                .ForMember(dest => dest.AirLine, opt => opt.MapFrom(src => src.Airline != null ? src.Airline.Name : string.Empty))
                .ForMember(dest => dest.FlightNumberIata, opt => opt.MapFrom(src => src.Flight != null ? src.Flight.Iata : string.Empty))
                .ReverseMap();

           
            CreateMap<AviationApiResponse, List<FlightViewDto>>()
                .ConvertUsing(src => src.Data.Select(flight => new FlightViewDto
                {
                    FlightDate = flight.FlightDate != null ? DateTime.Parse(flight.FlightDate) : DateTime.Today.AddDays(rnd.Next(1, 100)),
                    DepartureAirport = flight.Departure != null ? flight.Departure.Airport : string.Empty,
                    ArrivalAirport = flight.Arrival != null ? flight.Arrival.Airport : string.Empty,
                    AirLine = flight.Airline != null ? flight.Airline.Name : string.Empty,
                    FlightNumberIata = flight.Flight != null ? flight.Flight.Iata : string.Empty
                }).ToList());
        }
	}
}

