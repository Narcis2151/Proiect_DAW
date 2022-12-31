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

        //[Authorize(Roles = "User, Admin")] 
        public IActionResult Show(string id)
        {
            int numar = db.Profiles.Include("ApplicationUser").Where(prof => prof.ApplicationUserId == id).Count();

            
            Profile profile = new Profile();
            if (numar != 0)
            {
                profile = db.Profiles.Include("ApplicationUser")
                                            .Where(prof => prof.ApplicationUserId == id)
                                            .First();
            }

            if (id == _userManager.GetUserId(User))
            {
                if(numar != 0)
                {
                    ViewData["IsOwn"] = "yes";
                    int numar_postari = db.ApplicationUsers.Include("Profile").Include("Posts").Where(user => user.ApplicationUserId == id).Count();

                    return View(profile);

                    
                }

                else 
                {
                    return Redirect("/Identity/Account/Manage");
                }
            }
            else
            {
                if(numar != 0)
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
