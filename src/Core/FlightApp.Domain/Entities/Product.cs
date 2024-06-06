using System;
using FlightApp.Domain.Common;

namespace FlightApp.Domain.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public decimal Value { get; set; }
		public int Quantity { get; set; }
	}
}

