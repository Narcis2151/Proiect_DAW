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

        public IActionResult Show(string id)
        {
            int numar = db.Profiles.Include("ApplicationUser").Where(prof => prof.ApplicationUserId == id).Count();

            Profile profile = new();
            if (TempData.ContainsKey("message_access"))
            {
                ViewBag.Msg = TempData["message_access"].ToString();
            }

            if (numar != 0)
            {
                profile = db.Profiles.Include("ApplicationUser")
                                              .Where(prof => prof.ApplicationUserId == id)
                                              .First();
            }

            if (id == _userManager.GetUserId(User))
            {
                if(numar == 0)
                {
                    TempData["message_exist"] = "You currently don't have a profile. Create a profile to connect with other users.";
                    return Redirect("/Identity/Account/Manage");
                }

                else 
                {
                    ViewData["IsOwn"] = "yes";
                    return View(profile);
                }
            }
            else
            {
                if(numar == 1)
                {
                    ViewData["IsOwn"] = "no";
                    return View(profile);   
                }
                else
                {
                    TempData["message_access"] = "The profile you're trying to access does not exist. Redirecting to your profile.";
                    return Redirect("/Profiles/Show/"+_userManager.GetUserId(User));
                }
            }

        }

        

    }
}
