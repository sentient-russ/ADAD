using adad.Models;
using adad.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Policy;

namespace adad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*UpdateAllLatLong results in check for "" latitude and longitudes and updates them if possible. Only meant to be run once* Gets data from Google API. This is real $$ expensive so please do not use*/
            /*UpdateAllLatLong();*/


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
    }
}