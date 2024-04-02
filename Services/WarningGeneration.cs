using adad.Controllers;
using adad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
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
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(3600));
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
            UpdateWarnings();
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
        public async void UpdateWarnings()
        {
            /*UpdateAllLatLong results in check for "" latitude and longitudes and updates them if possible. Only meant to be run once* Gets data from Google API. This is real $$ expensive so please do not use*/
            /*UpdateAllLatLong();*/
            DataAccess data = new DataAccess();
            WarningGeneration warnings = new WarningGeneration();
            List<SiteModel> sitesWithWarnings = await warnings.CheckForHurricanes();
            for (int i = 0; i < sitesWithWarnings.Count; i++)
            {
                data.UpdateSite(sitesWithWarnings[i]);
            }
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
            if(siteResultModel.threat != "N/A")
            {
                // send warning email
                ContactDataModel newContact = new ContactDataModel();
                EmailService email = new EmailService();
                newContact.Email = siteResultModel.email;
                newContact.Name = siteResultModel.contact_name;
                newContact.Message = "The ADAD weather system detected a weather event code: " + siteResultModel.threat;
                newContact.Subject = "Important warning from ADAD system";
                email.SendWarningMessage(newContact);
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

    }
}
