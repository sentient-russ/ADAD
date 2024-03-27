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
/*        [HttpGet]
        [Authorize]
        [Route("Portal/Edit/{idSite}")]
        public async Task<IActionResult> Edit([FromRoute] string? idSite)
        {
            DataAccess dataAccess = new DataAccess();

        }*/
    }
}

