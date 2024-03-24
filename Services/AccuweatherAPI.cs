using adad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace adad.Services
{
    [ApiController]
    [Route("[Controller]")]
    public class AccuweatherAPI : ControllerBase
    {

        public string Accuweather_Key;

        public AccuweatherAPI()
        {

            string Accuweather_Key = Environment.GetEnvironmentVariable("Accuweather_Key");
        }
        /*
         * Returns the latitude and longitude in a SiteModel object from accuweathers API
         * @param SiteModelIn the model containing the site to be searche for
         * @return the site model with a latitude and longitude if found         
         */           
        [HttpGet]
        public async Task<SiteModel>? GetLatLong(SiteModel SiteModelIn)
        {
            SiteModel? siteResultModel = new SiteModel();
            string Accuweather_Key = Environment.GetEnvironmentVariable("Accuweather_Key");

            string uri = "http://dataservice.accuweather.com/locations/v1/cities/" + SiteModelIn.country_id + "/search?apikey=" + Accuweather_Key + "&q=Chongqing&language=en-us&details=false&offset=1&alias=Never";
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Method = "GET";
            HttpWebResponse? webResponse = (HttpWebResponse)webRequest.GetResponse();
            string responseString = "";
            using (Stream stream = webResponse.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                responseString = sr.ReadToEnd();
                sr.Close();
            }
            var data = JsonSerializer.Deserialize<List<GeoPositionJson>>(responseString);

            Console.WriteLine(data[0].geoPosition.longitude);



                /*siteResultModel = JsonSerializer.Deserialize<SiteModel>(Result.Result);*/
                Debug.WriteLine($"Requested Site Name: {SiteModelIn.site_name}");
                Debug.WriteLine($"Requested City: {SiteModelIn?.city}");
                Debug.WriteLine($"Found Latitude: {data[0].geoPosition.latitude}");
                Debug.WriteLine($"Found Longitude: {data[0].geoPosition.latitude}");

                siteResultModel.idSite = SiteModelIn.idSite;
                siteResultModel.site_name = SiteModelIn.site_name;
                siteResultModel.country = SiteModelIn.country;
                siteResultModel.city = SiteModelIn.city;
                siteResultModel.latitude = data[0].geoPosition.latitude.ToString();
                siteResultModel.longitude = data[0].geoPosition.longitude.ToString();
                siteResultModel.contact_name = SiteModelIn.contact_name;
                siteResultModel.country_code = SiteModelIn.country_code;
                siteResultModel.phone = SiteModelIn.phone;
                siteResultModel.sms = SiteModelIn.sms;
                siteResultModel.email = SiteModelIn.email;

                return siteResultModel;
        }
    }
}

