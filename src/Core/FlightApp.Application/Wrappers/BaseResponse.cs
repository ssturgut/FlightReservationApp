using System;
namespace FlightApp.Application.Wrappers
{
	public class BaseResponse
	{
		public Guid Id { get; set; }
		public bool Success { get; set; }
		public String Message { get; set; }
	}
}

