using adad.Models;
using Azure.Core;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using Microsoft.AspNet.SignalR.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace adad.Services
{
    public class GoogleLatLong_Service
    {

        public string Google_Maps_API_Key;

        public GoogleLatLong_Service()
        {

            string Google_Maps_API_Key = Environment.GetEnvironmentVariable("Google_Maps_API_Key");
        }
        /*
         * Returns the latitude and longitude in a SiteModel object from Google Maps API
         * @param SiteModelIn the model containing the site to be searche for
         * @return the site model with a latitude and longitude if found         
         */
        [HttpGet]
        public async Task<SiteModel>? GetLatLong(SiteModel SiteModelIn)
        {
            SiteModel? siteResultModel = new SiteModel();
            string Google_Maps_API_Key = Environment.GetEnvironmentVariable("Google_Maps_API_Key");


            string uri = "https://maps.googleapis.com/maps/api/geocode/json?address=" + SiteModelIn.city + "%20" + SiteModelIn.country + "&country=" + SiteModelIn.country_id + "&key=" + Google_Maps_API_Key + "&q=Chongqing&language=en-us&details=false&offset=1&alias=Never";
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Method = "GET";
            HttpWebResponse? webResponse = (HttpWebResponse)webRequest.GetResponse();
            string responseString;
            using (Stream stream = webResponse.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                responseString = sr.ReadToEnd();
                sr.Close();
            }
            Root data = JsonConvert.DeserializeObject<Root>(responseString);



            //APIOpenModel data = JsonConvert.DeserializeObject<APIOpenModel>(responseString);
            Console.WriteLine(data);
            //Console.WriteLine(data.results[0].geometry.location.lat.ToString());



            return siteResultModel;
        }
        public class Hourly
        {
            public List<string> time { get; set; }
            public List<double> temperature_2m { get; set; }
            public List<double> precipitation { get; set; }
            public List<int> weather_code { get; set; }
            public List<double> wind_speed_10m { get; set; }
            public List<int> wind_direction_10m { get; set; }
        }

        public class HourlyUnits
        {
            public string time { get; set; }
            public string temperature_2m { get; set; }
            public string precipitation { get; set; }
            public string weather_code { get; set; }
            public string wind_speed_10m { get; set; }
            public string wind_direction_10m { get; set; }
        }

        public class Root
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public double generationtime_ms { get; set; }
            public int utc_offset_seconds { get; set; }
            public string timezone { get; set; }
            public string timezone_abbreviation { get; set; }
            public double elevation { get; set; }
            public HourlyUnits hourly_units { get; set; }
            public Hourly hourly { get; set; }
        }
    }
}
