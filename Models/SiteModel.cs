using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using System.Text.Json.Serialization;
using System.Configuration;

namespace adad.Models
{

    [ApiController]
    [Route("[Controller]")]
    [BindProperties(SupportsGet = true)]

    public class SiteModel
    {
        [Key]
        [Required]
        [BindProperty(SupportsGet = true, Name = "idSite")]
        [DisplayName("Site ID")]
        public string? idSite { get; set; } = "";

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [BindProperty(SupportsGet = true, Name = "site_name")]
        [DisplayName("Site Name")]
        public string? site_name { get; set; } = "";

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [BindProperty(SupportsGet = true, Name = "country")]
        [DisplayName("Site Country")]
        public string? country { get; set; } = "";

        [Required]
        [StringLength(2, MinimumLength = 2)]
        [BindProperty(SupportsGet = true, Name = "country_id")]
        [DisplayName("Site Country ID")]
        public string? country_id { get; set; } = "";

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [BindProperty(SupportsGet = true, Name = "city")]
        [DisplayName("Site City")]
        public string? city { get; set; } = "";

        [Required]
        [Range(-100, float.MaxValue)]
        [BindProperty(SupportsGet = true, Name = "latitude")]
        [DisplayName("Site Latitude")]
        public string? latitude { get; set; } = "";

        [Required]
        [Range(-100, float.MaxValue)]
        [BindProperty(SupportsGet = true, Name = "longitude")]
        [DisplayName("Site Longitude")]
        public string? longitude { get; set; } = "";

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [BindProperty(SupportsGet = true, Name = "contact_name")]
        [DisplayName("Contact Name")]
        public string? contact_name { get; set; } = "";

        [Required]
        [StringLength(3, MinimumLength = 1)]
        [BindProperty(SupportsGet = true, Name = "country_code")]
        [DisplayName("Phone Country Code")]
        public string? country_code { get; set; } = "";

        [Required]
        [StringLength(11, MinimumLength = 9)]
        [BindProperty(SupportsGet = true, Name = "phone")]
        [DisplayName("Site Contact Number")]
        public string? phone { get; set; } = "";

        [Required]
        [StringLength(11, MinimumLength = 9)]
        [BindProperty(SupportsGet = true, Name = "sms")]
        [DisplayName("Site SMS")]
        public string? sms { get; set; } = "";

        [Required]
        [EmailAddress]
        [BindProperty(SupportsGet = true, Name = "email")]
        [DisplayName("Site Email")]
        public string? email { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "threat")]
        [DisplayName("Threat Type")]
        public string? threat { get; set; } = "N/A";

        [BindProperty(SupportsGet = true, Name = "severity")]
        [DisplayName("Severity Level")]
        public string? severity { get; set; } = "N/A";

        [BindProperty(SupportsGet = true, Name = "winddir")]
        [DisplayName("6hrs Wind Dir.")]
        public string? wind_direction { get; set; } = "N/F";

        [BindProperty(SupportsGet = true, Name = "windspeed")]
        [DisplayName("6hrs Wind Spd.")]
        public string? wind_speed { get; set; } = "N/F";

        [BindProperty(SupportsGet = true, Name = "send_warning")]
        [DisplayName("Email this address in the event of any threat.")]
        public bool send_warning { get; set; } = false;

        [BindProperty(SupportsGet = true, Name = "curr_time")]
        [DisplayName("Update Time")]
        public string? curr_time { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "curr_weather_code")]
        [DisplayName("Current Condition")]
        public string? curr_weather_code { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "curr_temperature")]
        [DisplayName("Current Temp.")]
        public string? curr_temperature { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "curr_precip")]
        [DisplayName("Current Precipitation")]
        public string? curr_precip { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "curr_windspeed")]
        [DisplayName("Current Wind Speed")]
        public string? curr_windspeed { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "curr_gusts")]
        [DisplayName("Current Gust Speed")]
        public string? curr_gusts { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "curr_wind_dir")]
        [DisplayName("Current Wind Direction")]
        public string? curr_wind_dir { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "tomorrow_weather_code")]
        [DisplayName("Tomorrow's Condition")]
        public string? tomorrow_weather_code { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "tomorrow_high_temp")]
        [DisplayName("Tomorrow's High Temp.")]
        public string? tomorrow_high_temp { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "tomorrow_low_temp")]
        [DisplayName("Tomorrow's Low Temp.")]
        public string? tomorrow_low_temp { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "tomorrow_precip")]
        [DisplayName("Tomorrow's Precipitation")]
        public string? tomorrow_precip { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "tomorrow_windspeed")]
        [DisplayName("Tomorrow's Wind Speed")]
        public string? tomorrow_windspeed { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "tomorrow_gusts")]
        [DisplayName("Tomorrow's Gust Speed")]
        public string? tomorrow_gusts { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "tomorrow_wind_dir")]
        [DisplayName("Tomorrows Wind Direction")]
        public string? tomorrow_wind_dir { get; set; } = "";


    }
}