// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.ComponentModel.DataAnnotations.Schema;

[NotMapped]
public class GoogleLatLong
{
    public string long_name { get; set; }
    public string short_name { get; set; }
    public List<string> types { get; set; }
}
[NotMapped]
public class Bounds
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}
[NotMapped]
public class Geometry
{
    public Bounds bounds { get; set; }
    public Location location { get; set; }
    public string location_type { get; set; }
    public Viewport viewport { get; set; }
}
[NotMapped]
public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
}
[NotMapped]
public class Northeast
{
    public double lat { get; set; }
    public double lng { get; set; }
}
[NotMapped]
public class Result
{
    public List<GoogleLatLong> address_components { get; set; }
    public string formatted_address { get; set; }
    public Geometry geometry { get; set; }
    public string place_id { get; set; }
    public List<string> types { get; set; }
}
[NotMapped]
public class Root
{
    public List<Result> results { get; set; }
    public string status { get; set; }
}
[NotMapped]
public class Southwest
{
    public double lat { get; set; }
    public double lng { get; set; }
}
[NotMapped]
public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

