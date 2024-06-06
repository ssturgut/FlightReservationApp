using System;
using FlightApp.Domain.Common;

namespace FlightApp.Domain.Entities
{
	public class Client : BaseEntity
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string PassportNumber { get; set; }
		public string EMail { get; set; }
		public string Password { get; set; }
	}
}

