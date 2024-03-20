using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace adad.Models
{
    [ApiController]
    [Route("[Controller]")]
    [BindProperties(SupportsGet = true)]
    public class SiteModel
    {
        [Key]
        [BindProperty(SupportsGet = true, Name = "idSite")]
        [DisplayName("Site ID")]
        public string? idSite { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "site_name")]
        [DisplayName("Site Name")]
        public string? site_name { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "country")]
        [DisplayName("Site Country")]
        public string? country { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "city")]
        [DisplayName("Site City")]
        public string? city { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "latitude")]
        [DisplayName("Site Latitude")]
        public string? latitude { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "longitude")]
        [DisplayName("Site Longitude")]
        public string? longitude { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "contact_name")]
        [DisplayName("Contact Name")]
        public string? contact_name { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "phone")]
        [DisplayName("Site Contact Number")]
        public string? phone { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "sms")]
        [DisplayName("Site SMS")]
        public string? sms { get; set; } = "";

        [BindProperty(SupportsGet = true, Name = "email")]
        [DisplayName("Site Email")]
        public string? email { get; set; } = "";

    }

}