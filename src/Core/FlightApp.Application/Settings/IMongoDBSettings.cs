using System;
namespace FlightApp.Application.Settings
{
    public interface IMongoDBSettings
    {
        //string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

