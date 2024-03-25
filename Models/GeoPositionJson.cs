using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
//This class is only used for the serialization of inbound lat long data using the accuweather API.  Do not use or disturb.
namespace adad.Models
{
    class GeoPositionJson
    {
        [JsonPropertyName("version")]
        public int version { get; set; }

        [JsonPropertyName("Key")]
        public string key { get; set; }

        [JsonPropertyName("Type")]
        public string type { get; set; }

        [JsonPropertyName("Rank")]
        public int rank { get; set; }

        [JsonPropertyName("LocalizedName")]
        public string localizedName { get; set; }

        [JsonPropertyName("EnglishName")]
        public string englishName { get; set; }

        [JsonPropertyName("PrimaryPostalCode")]
        public string primaryPostalCode { get; set; }

        [JsonPropertyName("GeoPosition")]
        public GeoPosition geoPosition { get; set; }

      
        public class GeoPosition
        {
            [JsonPropertyName("Latitude")]
            public double latitude { get; set; } = 0;

            [JsonPropertyName("Longitude")]
            public double longitude { get; set; } = 0;
        }
    }
}
