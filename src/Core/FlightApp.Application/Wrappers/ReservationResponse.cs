namespace FlightApp.Application.Wrappers
{
    public class ReservationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ReservationResponse() { }

        public ReservationResponse(object data)
        {
            Success = true;
            Message = string.Empty;
            Data = data;
        }

        public ReservationResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}


