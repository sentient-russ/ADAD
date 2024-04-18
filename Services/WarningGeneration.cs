using adad.Controllers;
using adad.Models;
﻿using adad.Migrations;
using adad.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using static System.Net.WebRequestMethods;

namespace adad.Services
{
    public class WarningGeneration : IHostedService, IDisposable
    {
        private Timer? _timer;

        public WarningGeneration()
        {            
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(3600)); //1hr x 24hrs x 350 calls = 8400 calls per day and within the 10000 calls per day limit.
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
        private void DoWork(object? state)
        {
            //UpdateWarnings();
            UpdateWeatherDetails();
        }
        public async void UpdateWarnings()
        {
            DataAccess data = new DataAccess();
            WarningGeneration warnings = new WarningGeneration();
            List<SiteModel> sitesWithWarnings = await warnings.CheckForHurricanes();
            for (int i = 0; i < sitesWithWarnings.Count; i++)
            {
                data.UpdateSite(sitesWithWarnings[i]);

            }
        }
        public async void UpdateWeatherDetails()
        {           
            DataAccess data = new DataAccess();
            List<SiteModel> allSites = new List<SiteModel>();
            allSites = data.GetSites();
            for (int i = 0; i < allSites.Count; i++)
            {
                SiteModel checkedSite = await CurrentWeatherCheck(allSites[i]);
                data.UpdateSite(checkedSite);
                Console.WriteLine("Updated Site: " +  checkedSite.idSite );

            }
        }
        public async Task<List<SiteModel>> CheckForHurricanes()
        {
            DataAccess data = new DataAccess();
            List<SiteModel> allSites = new List<SiteModel> ();
            allSites = data.GetSites();
            for(int i = 0; i < allSites.Count; i++)
            {
                SiteModel checkedSite = await HurricaneCheck(allSites[i]);
                if(checkedSite.threat != "N/A") { allSites[i].threat = checkedSite.threat; }
                if (checkedSite.severity != "N/A") { allSites[i].severity = checkedSite.severity; }
                allSites[i].wind_speed = checkedSite.wind_speed;
                allSites[i].wind_direction = checkedSite.wind_direction;
            }
            return allSites;
        }

        /*
        * Returns wind speed and wind direction for each corridinate pair.
        * @param SiteModelIn this is the site that the coordinate pair will be chected for
        * @return the site model with a latitude and longitude if found         
        */
        [HttpGet]
        public async Task<SiteModel>? HurricaneCheck(SiteModel SiteModelIn)
        {


            
            
            SiteModel? siteResultModel = new SiteModel();
            string uri = "https://api.open-meteo.com/v1/forecast?latitude=" + SiteModelIn.latitude + "&longitude=" + SiteModelIn.longitude + "&hourly=temperature_2m,precipitation,weather_code,wind_speed_10m,wind_direction_10m&temperature_unit=fahrenheit&wind_speed_unit=mph&precipitation_unit=inch&timezone=America%2FNew_York&forecast_days=3";
            
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

            string currentHour = DateTime.Now.ToString("HH");
            int futureHour = Int32.Parse(currentHour) + 6;
            double futureWindSpeeed = Double.Parse(data.hourly.wind_speed_10m[futureHour].ToString());
            int futureWindDirection = Int32.Parse(data.hourly.wind_direction_10m[futureHour].ToString());
            string predictedWindDirection = "";
            if (futureWindDirection > 337.5 || futureWindDirection <= 22.5) { predictedWindDirection = "North"; }
            else if (futureWindDirection > 22.5 && futureWindDirection <= 67.5 ) { predictedWindDirection = "North East"; }
            else if (futureWindDirection > 67.5 && futureWindDirection <= 112.5) { predictedWindDirection = "East"; }
            else if (futureWindDirection > 112.5 && futureWindDirection <= 157.5) { predictedWindDirection = "South East"; }
            else if (futureWindDirection > 157.5 && futureWindDirection <= 247.5) { predictedWindDirection = "South"; }
            else if (futureWindDirection > 247.5 && futureWindDirection <= 282.5) { predictedWindDirection = "South West"; }
            else if (futureWindDirection > 282.5 && futureWindDirection <= 337.5) { predictedWindDirection = "West"; }
            else { predictedWindDirection = "North West"; }
            if(futureWindSpeeed >= 74)
            {
                siteResultModel.threat = "Hurricane";
                siteResultModel.severity = "High";
                
            }
            
            siteResultModel.wind_speed = futureWindSpeeed.ToString();
            siteResultModel.wind_direction = predictedWindDirection;
            if (siteResultModel.threat.CompareTo("N/A") != 0 && siteResultModel.send_warning)
            {
                // copy to russell@magnadigi.com for confirmation that the service is working.
                ContactDataModel newContact = new ContactDataModel();
                EmailService email = new EmailService();
                newContact.Email = "russell@magnadigi.com";
                newContact.Name = siteResultModel.contact_name;
                newContact.Message = "The ADAD weather system detected a weather event code: " + siteResultModel.threat;
                newContact.Subject = "Important warning from ADAD system";
                email.SendWarningMessage(newContact, siteResultModel);
            }

            return siteResultModel;
        }
        
        /* 
         * Returns various current weather properties for each corridinate pair.
         * @param SiteModelIn this is the site that the coordinate pair will be chected for
         * @return the site model with a all parameters if found
         */
        [HttpGet]
        public async Task<SiteModel>? CurrentWeatherCheck(SiteModel SiteModelIn)
        {
            SiteModel? siteResultModel = new SiteModel();
            try
            {


                Dictionary<string, string> weatherCodes = new Dictionary<string, string>();
                weatherCodes.Add("0", "Clear Sky");
                weatherCodes.Add("1", "Mainly clear, partly cloudy and overcast.");
                weatherCodes.Add("2", "Mainly clear, partly cloudy and overcast.");
                weatherCodes.Add("3", "Mainly clear, partly cloudy and overcast.");
                weatherCodes.Add("45", "Fog and depositing rime fog.");
                weatherCodes.Add("48", "Fog and depositing rime fog.");
                weatherCodes.Add("51", "Drizzle: Light, moderate and dense intensity.");
                weatherCodes.Add("53", "Drizzle: Light, moderate and dense intensity.");
                weatherCodes.Add("55", "Drizzle: Light, moderate and dense intensity.");
                weatherCodes.Add("56", "Freezing Drizzle: Light and dense intensity.");
                weatherCodes.Add("57", "Freezing Drizzle: Light and dense intensity.");
                weatherCodes.Add("61", "Rain: Slight, moderate and heavy intensity.");
                weatherCodes.Add("63", "Rain: Slight, moderate and heavy intensity.");
                weatherCodes.Add("65", "Rain: Slight, moderate and heavy intensity.");
                weatherCodes.Add("66", "Freezing Rain: Light and heavy intensity.");
                weatherCodes.Add("67", "Freezing Rain: Light and heavy intensity.");
                weatherCodes.Add("71", "Snow fall: Slight, moderate, and heavy intensity.");
                weatherCodes.Add("73", "Snow fall: Slight, moderate, and heavy intensity.");
                weatherCodes.Add("75", "Snow fall: Slight, moderate, and heavy intensity.");
                weatherCodes.Add("77", "Snow grains.");
                weatherCodes.Add("80", "Rain showers: Slight, moderate and violent.");
                weatherCodes.Add("81", "Rain showers: Slight, moderate and violent.");
                weatherCodes.Add("82", "Rain showers: Slight, moderate and violent.");
                weatherCodes.Add("85", "Snow showers slight and heavy.");
                weatherCodes.Add("86", "Snow showers slight and heavy.");
                weatherCodes.Add("95", "Thunderstorm: Slight or moderate.");
                weatherCodes.Add("96", "Thunderstorm: Slight or moderate.");
                weatherCodes.Add("99", "Thunderstorm with slight and heavy hail.");

                
                string uri = "https://api.open-meteo.com/v1/forecast?latitude=" + SiteModelIn.latitude + "&longitude=" + SiteModelIn.longitude + "&current=temperature_2m,precipitation,weather_code,wind_speed_10m,wind_direction_10m,wind_gusts_10m&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum,showers_sum,snowfall_sum,wind_speed_10m_max,wind_gusts_10m_max,wind_direction_10m_dominant&temperature_unit=fahrenheit&wind_speed_unit=mph&precipitation_unit=inch&timezone=America%2FNew_York";

                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.Method = "GET";

                HttpWebResponse? webResponse = (HttpWebResponse)webRequest.GetResponse();
                string responseStringCurrent = "";
                using (Stream stream = webResponse.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    responseStringCurrent = sr.ReadToEnd();
                    sr.Close();
                }

                CurrentRoot data = JsonConvert.DeserializeObject<CurrentRoot>(responseStringCurrent);

                siteResultModel = SiteModelIn;
                siteResultModel.curr_time = DateTime.Now.ToString();
                siteResultModel.curr_weather_code = data.current.weather_code.ToString();
                //lookup code description
                if (weatherCodes.ContainsKey(siteResultModel.curr_weather_code)) { siteResultModel.curr_weather_code = weatherCodes[siteResultModel.curr_weather_code]; }
                siteResultModel.curr_temperature = data.current.temperature_2m.ToString();
                siteResultModel.curr_precip = data.current.precipitation.ToString();
                siteResultModel.curr_windspeed = data.current.wind_speed_10m.ToString();
                siteResultModel.curr_gusts = data.current.wind_gusts_10m.ToString();
                //convert direction
                double CurrentWindDirection = data.current.wind_direction_10m;
                string currentWindDirection = "";
                if (CurrentWindDirection > 337.5 || CurrentWindDirection <= 22.5) { currentWindDirection = "North"; }
                else if (CurrentWindDirection > 22.5 && CurrentWindDirection <= 67.5) { currentWindDirection = "North East"; }
                else if (CurrentWindDirection > 67.5 && CurrentWindDirection <= 112.5) { currentWindDirection = "East"; }
                else if (CurrentWindDirection > 112.5 && CurrentWindDirection <= 157.5) { currentWindDirection = "South East"; }
                else if (CurrentWindDirection > 157.5 && CurrentWindDirection <= 247.5) { currentWindDirection = "South"; }
                else if (CurrentWindDirection > 247.5 && CurrentWindDirection <= 282.5) { currentWindDirection = "South West"; }
                else if (CurrentWindDirection > 282.5 && CurrentWindDirection <= 337.5) { currentWindDirection = "West"; }
                else { currentWindDirection = "North West"; }
                siteResultModel.curr_wind_dir = currentWindDirection;
                siteResultModel.tomorrow_weather_code = data.daily.weather_code[1].ToString();
                //lookup code description
                if (weatherCodes.ContainsKey(siteResultModel.tomorrow_weather_code)) { siteResultModel.tomorrow_weather_code = weatherCodes[siteResultModel.tomorrow_weather_code]; }
                siteResultModel.tomorrow_high_temp = data.daily.temperature_2m_max[1].ToString();
                siteResultModel.tomorrow_low_temp = data.daily.temperature_2m_min[1].ToString();
                siteResultModel.tomorrow_precip = data.daily.precipitation_sum[1].ToString();
                siteResultModel.tomorrow_windspeed = data.daily.wind_speed_10m_max[1].ToString();
                siteResultModel.tomorrow_gusts = data.daily.wind_gusts_10m_max[1].ToString();
                //convert direction
                siteResultModel.tomorrow_wind_dir = data.daily.wind_direction_10m_dominant[1].ToString();
                //convert direction
                double FutureWindDirection = data.current.wind_direction_10m;
                string predictedWindDirection = "";
                if (FutureWindDirection > 337.5 || FutureWindDirection <= 22.5) { predictedWindDirection = "North"; }
                else if (FutureWindDirection > 22.5 && FutureWindDirection <= 67.5) { predictedWindDirection = "North East"; }
                else if (FutureWindDirection > 67.5 && FutureWindDirection <= 112.5) { predictedWindDirection = "East"; }
                else if (FutureWindDirection > 112.5 && FutureWindDirection <= 157.5) { predictedWindDirection = "South East"; }
                else if (FutureWindDirection > 157.5 && FutureWindDirection <= 247.5) { predictedWindDirection = "South"; }
                else if (FutureWindDirection > 247.5 && FutureWindDirection <= 282.5) { predictedWindDirection = "South West"; }
                else if (FutureWindDirection > 282.5 && FutureWindDirection <= 337.5) { predictedWindDirection = "West"; }
                else { predictedWindDirection = "North West"; }
                siteResultModel.tomorrow_wind_dir = predictedWindDirection;
                if (Double.Parse(siteResultModel.tomorrow_gusts) >= 74 || Double.Parse(siteResultModel.curr_gusts) >= 74)
                {
                    siteResultModel.threat = "Hurricane";
                    siteResultModel.severity = "High";
                }
                else if (Double.Parse(siteResultModel.curr_temperature) <= 32 || Double.Parse(siteResultModel.tomorrow_low_temp) <= 32)
                {
                    siteResultModel.threat = "Feezing";
                    siteResultModel.severity = "Low";
                }
                else if (Double.Parse(siteResultModel.curr_temperature) >= 100 || Double.Parse(siteResultModel.tomorrow_high_temp) >= 100)
                {
                    siteResultModel.threat = "High Heat";
                    siteResultModel.severity = "Medium";
                }
                if (!siteResultModel.threat.Equals("N/A") && siteResultModel.send_warning)
                {
                    // send warning email
                    ContactDataModel newContact = new ContactDataModel();
                    EmailService email = new EmailService();
                    newContact.Email = siteResultModel.email;
                    newContact.Name = siteResultModel.contact_name;
                    newContact.Message = "The ADAD weather system detected a weather event code: " + siteResultModel.threat;
                    newContact.Subject = "Important warning from ADAD system";
                    email.SendWarningMessage(newContact, siteResultModel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return siteResultModel;

        }

        public async void UpdateAllLatLong()
        {
            DataAccess dataAccess = new DataAccess();
            GoogleLatLong_Service api = new GoogleLatLong_Service();
            List<SiteModel> allSites = new List<SiteModel>();
            allSites = dataAccess.GetSites();


            for (int i = 0; i < allSites.Count; i++)
            {
                if (allSites[i].latitude == "" || allSites[i].longitude == "")
                {
                    SiteModel updateSite = allSites[i];
                    SiteModel updatedSite = await api.GetLatLong(updateSite);
                    Console.WriteLine(updatedSite);
                    dataAccess.UpdateSiteLatLng(updatedSite);
                }
            }
        }

        //JsonConvert model for hurricane inforamtion
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

        public class Current
        {
            public string time { get; set; }
            public int interval { get; set; }
            public double temperature_2m { get; set; }
            public double precipitation { get; set; }
            public int weather_code { get; set; }
            public double wind_speed_10m { get; set; }
            public int wind_direction_10m { get; set; }
            public double wind_gusts_10m { get; set; }
        }

        public class CurrentUnits
        {
            public string time { get; set; }
            public string interval { get; set; }
            public string temperature_2m { get; set; }
            public string precipitation { get; set; }
            public string weather_code { get; set; }
            public string wind_speed_10m { get; set; }
            public string wind_direction_10m { get; set; }
            public string wind_gusts_10m { get; set; }
        }

        public class Daily
        {
            public List<string> time { get; set; }
            public List<int> weather_code { get; set; }
            public List<double> temperature_2m_max { get; set; }
            public List<double> temperature_2m_min { get; set; }
            public List<double> precipitation_sum { get; set; }
            public List<double> rain_sum { get; set; }
            public List<double> showers_sum { get; set; }
            public List<double> snowfall_sum { get; set; }
            public List<double> wind_speed_10m_max { get; set; }
            public List<double> wind_gusts_10m_max { get; set; }
            public List<int> wind_direction_10m_dominant { get; set; }
        }

        public class DailyUnits
        {
            public string time { get; set; }
            public string weather_code { get; set; }
            public string temperature_2m_max { get; set; }
            public string temperature_2m_min { get; set; }
            public string precipitation_sum { get; set; }
            public string rain_sum { get; set; }
            public string showers_sum { get; set; }
            public string snowfall_sum { get; set; }
            public string wind_speed_10m_max { get; set; }
            public string wind_gusts_10m_max { get; set; }
            public string wind_direction_10m_dominant { get; set; }
        }

        public class CurrentRoot
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public double generationtime_ms { get; set; }
            public int utc_offset_seconds { get; set; }
            public string timezone { get; set; }
            public string timezone_abbreviation { get; set; }
            public double elevation { get; set; }
            public CurrentUnits current_units { get; set; }
            public Current current { get; set; }
            public DailyUnits daily_units { get; set; }
            public Daily daily { get; set; }
        }


    }
}
