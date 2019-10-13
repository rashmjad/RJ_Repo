using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CityWeather.Models;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace CityWeather.BLL
{
    public class WeatherService
    {
        public List<Report> FetchWeather(WeatherDTO data)
        {
            string WeatherAppId = ConfigurationManager.AppSettings["WeatherAppId"];
            string apiResponse = "";
            List<Report> reportList = new List<Report>();

            try
            {
                foreach (var id in data.CityIds)
                {
                    try
                    {
                        HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(@"http://api.openweathermap.org/data/2.5/weather?id=" + id + "&appid=" + WeatherAppId + "&units=metric");
                        using (HttpWebResponse httpResponse = (HttpWebResponse)apiRequest.GetResponse())
                        {
                            using (Stream reader = httpResponse.GetResponseStream())
                            {
                                apiResponse = (new StreamReader(reader)).ReadToEnd();
                            }
                            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(apiResponse);
                            if (weatherResponse != null)
                            {
                                Report report = new Report
                                {
                                    City = weatherResponse.name,
                                    Id = id,
                                    Description = weatherResponse.weather.FirstOrDefault().description,
                                    Temp = weatherResponse.main.temp,
                                    Max_Temp = weatherResponse.main.temp_max,
                                    Min_Temp = weatherResponse.main.temp_min
                                };
                                reportList.Add(report);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception)
            { 
                
            }
            return reportList;
        }
    }      
}