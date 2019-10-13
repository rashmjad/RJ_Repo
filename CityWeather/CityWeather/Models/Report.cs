using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityWeather.Models
{
    public class Report
    {
        public string City { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public double Temp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public float Max_Temp { get; set; }
        public float Min_Temp { get; set; }
    }
}