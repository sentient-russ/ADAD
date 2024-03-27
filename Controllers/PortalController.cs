using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using adad.Models;
using adad.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using adad.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace adad.Controllers
{
    [BindProperties(SupportsGet = true)]
    public class PortalController : Controller

    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _env;

        public PortalController(UserManager<ApplicationUser> userManager,ApplicationDbContext context, IWebHostEnvironment env)
        {
            this._userManager = userManager;
            this._context = context;
            this._env = env;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            var firstName = currentUser.FirstName;
            var lastName = currentUser.LastName;
            var email = currentUser.Email;
            ViewModelBundle data = new ViewModelBundle();
            data.FirstName = firstName;
            data.LastName = lastName;
            data.Email = email;
    
            DataAccess dataAccess = new DataAccess();
            List<SiteModel> siteModels = dataAccess.GetSites();

            for (int i = 0; i < siteModels.Count; i++)
            {
                siteModels[i].idSite = "mid" + siteModels[i].idSite; 

            }
            data.sites = siteModels;

            return View(data);

        }

        [HttpGet]
        [Authorize]
        [Route("Portal/Edit/{idSite}")]
        public async Task<IActionResult> Edit([FromRoute] string? idSite)
        {
            DataAccess dataAccess = new DataAccess();
            SiteModel siteModel = dataAccess.GetSite(idSite);
            return View(siteModel); //edit current site view
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([Bind("idSite, site_name, country, country_id, city, latitude, longitude, contact_name, country_code, phone, sms, email, threat, severity")] SiteModel siteIn)
        {
            
            //save updated site
            DataAccess dataAccess = new DataAccess();
            SiteModel updatedSite = dataAccess.UpdateSite(siteIn);


            return RedirectToAction(nameof(Index)); //back to index
        }

        [HttpGet]
        [Authorize]
        [Route("Portal/Delete/{idSite}")]
        public async Task<IActionResult> Delete([FromRoute] string? idSite)
        {
            DataAccess dataAccess = new DataAccess();
            SiteModel siteModel = dataAccess.GetSite(idSite);
            bool result = dataAccess.DeleteSite(siteModel);
            return RedirectToAction(nameof(Index)); //confirmation view
        }

        [HttpGet]
        [Authorize]
        [Route("Portal/Create/")]
        public async Task<IActionResult> Create()
        {
            DataAccess dataAccess = new DataAccess();
            int nextId = dataAccess.GetSiteId();
            SiteModel newSite = new SiteModel();
            newSite.idSite = nextId.ToString();

            //needs to update with next available site number

            return View(newSite); //create new site view
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNew([Bind("idSite, site_name, country, country_id, city, latitude, longitude, contact_name, country_code, phone, sms, email, threat, severity")] SiteModel siteIn)
        {

            //save new site
            DataAccess dataAccess = new DataAccess();
            SiteModel updatedSite = dataAccess.InsertSiteDB(siteIn);


            return RedirectToAction(nameof(Index)); //back to index
        }
    }
}

