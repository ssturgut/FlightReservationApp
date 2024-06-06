using System;


namespace FlightApp.Domain.Common
{
	public class BaseEntity
	{
        public Guid Id { get; set; }
		public DateTime CreateDate { get; set; }
	}
}

