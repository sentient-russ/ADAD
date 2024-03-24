using adad.Models;
using Azure.Core;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

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
            string responseString = "";
            using (Stream stream = webResponse.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                responseString = sr.ReadToEnd();
                sr.Close();
            }
          
            Root data = JsonConvert.DeserializeObject<Root>(responseString);

            Console.WriteLine(data.results[0].geometry.location.lat.ToString());


                        Debug.WriteLine($"Requested Site Name: {SiteModelIn.site_name}");
                        Debug.WriteLine($"Requested City: {SiteModelIn?.city}");
                        Debug.WriteLine($"Found Latitude: {data.results[0].geometry.location.lat}");
                        Debug.WriteLine($"Found Longitude: {data.results[0].geometry.location.lng}");

                        siteResultModel.idSite = SiteModelIn.idSite;
                        siteResultModel.site_name = SiteModelIn.site_name;
                        siteResultModel.country = SiteModelIn.country;
                        siteResultModel.city = SiteModelIn.city;
                        siteResultModel.latitude = data.results[0].geometry.location.lat.ToString();
                        siteResultModel.longitude = data.results[0].geometry.location.lng.ToString();
                        siteResultModel.contact_name = SiteModelIn.contact_name;
                        siteResultModel.country_code = SiteModelIn.country_code;
                        siteResultModel.phone = SiteModelIn.phone;
                        siteResultModel.sms = SiteModelIn.sms;
                        siteResultModel.email = SiteModelIn.email;

            return siteResultModel;
        }
    }
}
