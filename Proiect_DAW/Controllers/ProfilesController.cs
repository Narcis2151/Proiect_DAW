using Proiect_DAW.Data;
using Proiect_DAW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Proiect_DAW.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfilesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(string id)
        {
            int numar = db.Profiles.Include("ApplicationUser").Where(prof => prof.ApplicationUserId == id).Count();
            if (numar == 0)
            {
                return RedirectToAction("New");
            }

            Profile profile = db.Profiles.Include("ApplicationUser")
                                          .Where(prof => prof.ApplicationUserId == id)
                                          .First();

            return View(profile);
        }

        public IActionResult New() {

            Profile profile = new Profile();

            profile.ApplicationUserId = _userManager.GetUserId(User);
            return View(profile);
        }
    }
}
