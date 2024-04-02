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

        [BindProperty(SupportsGet = true, Name = "windsp")]
        [DisplayName("6hrs Wind Spd.")]
        public string? wind_speed { get; set; } = "N/F";

        public bool SendWarning { get; set; }
    }
}