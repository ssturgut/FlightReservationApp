using System;
namespace FlightApp.Application.Wrappers
{
	public class LoginResponse
	{
		public bool IsSuccessful { get; set; }
		public string Token { get; set; }
		public string ErrorMessage { get; set; }
	}
}

