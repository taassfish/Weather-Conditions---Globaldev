using System;

namespace Weather_Conditions___Globaldev
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string City { get; set; }

    }


    public class WeatherForecastSI
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string City { get; set; }

        public decimal AverageT { get; set; }
        public int MinT { get; set; }
        public int MaxT { get; set; }

    }
}
