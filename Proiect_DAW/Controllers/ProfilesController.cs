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

        [Authorize(Roles = "User,Admin")]
        public IActionResult Show(string id)
        {

            if (TempData.ContainsKey("message_access"))
            {
                ViewBag.Msg = TempData["message_access"].ToString();
            }

            int numar = db.Profiles.Include("ApplicationUser").Where(prof => prof.ApplicationUserId == id).Count();

            
            Profile profile = new Profile();
            if (numar != 0)
            {
                profile = db.Profiles.Where(prof => prof.ApplicationUserId == id)
                                      .Include(prof => prof.ApplicationUser)
                                      .ThenInclude(us => us.Posts)
                                      .FirstOrDefault();
            }

            if (id == _userManager.GetUserId(User))
            {
                if(numar != 0)
                {
                    SetAccessRights(id);

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
                    SetAccessRights(id);
                    return View(profile);   
                }
                else
                {
                    TempData["message_access"] = "The profile you're trying to access does not exist. Redirecting to your profile.";
                    return RedirectToAction("Show", new { id = _userManager.GetUserId(User) });
                }
            }

        }

        public IActionResult Search(string id)
        {
            var profiles = from p in db.Profiles
                           select p;

            if (!String.IsNullOrEmpty(id))
            {
                profiles = profiles.Where(p => p.FirstName.Contains(id) || p.LastName.Contains(id) || p.Username.Contains(id));
            }

            foreach(Profile profile in profiles)
            {
                ViewData[profile.ApplicationUserId] = SetButtonsSearch(profile.ApplicationUserId);
            }
            ViewBag.Profiles = profiles;

            return View();
        }
        private void SetAccessRights(string id)
        {
            if(_userManager.GetUserId(User) == id) 
            {
                    ViewBag.Butoane = "Propriu";
                ViewBag.Viz = "Public";
            }
            else 
            {
                var friendships_1 = db.Friendships.Where(req => req.Requester.Id == id && req.Status == "Accepted" && req.Adresee.Id == _userManager.GetUserId(User))
                                                    .Include(req => req.Requester)
                                                    .ThenInclude(us => us.Profile)
                                                    .Include(req => req.Adresee)
                                                    .ThenInclude(us2 => us2.Profile).Count();
                var friendships_2 = db.Friendships.Where(req => req.Adresee.Id == id && req.Status == "Accepted" && req.Requester.Id == _userManager.GetUserId(User))
                                                    .Include(req => req.Requester)
                                                    .ThenInclude(us => us.Profile)
                                                    .Include(req => req.Adresee)
                                                    .ThenInclude(us2 => us2.Profile).Count();
                if(friendships_1 !=0 || friendships_2 !=0)
                {
                    ViewBag.Butoane = "Prieten";
                    ViewBag.Viz = "Public";
                }

                else 
                {
                    var sent = db.Friendships.Where(req => req.Requester.Id == _userManager.GetUserId(User) && req.Status == "Sent" && req.Adresee.Id == id )
                                                   .Include(req => req.Requester)
                                                   .ThenInclude(us => us.Profile)
                                                   .Include(req => req.Adresee)
                                                   .ThenInclude(us2 => us2.Profile);

                    var received = db.Friendships.Where(req => req.Adresee.Id == _userManager.GetUserId(User) && req.Status == "Sent" && req.Requester.Id == id)
                                                   .Include(req => req.Requester)
                                                   .ThenInclude(us => us.Profile)
                                                   .Include(req => req.Adresee)
                                                   .ThenInclude(us2 => us2.Profile);

                    if (sent.Count() !=0)
                    {
                        ViewBag.Butoane = "Trimis";
                        if (sent.First().Adresee.Profile.IsPrivate == true)
                            ViewBag.Viz = "Privat";
                        else
                            ViewBag.Viz = "Public";
                    }


                    else if(received.Count() != 0)
                    {
                        ViewBag.Butoane = "Primit";
                        if (received.First().Requester.Profile.IsPrivate == true)
                            ViewBag.Viz = "Privat";
                        else
                            ViewBag.Viz = "Public";
                    }

                    else
                    {
                        ViewBag.Butoane = "Nimic";
                        var profile = db.ApplicationUsers.Where(usr => usr.Id == id).Include(usr => usr.Profile).First();
                        if (profile.Profile.IsPrivate == true)
                            ViewBag.Viz = "Privat";
                        else
                            ViewBag.Viz = "Public";
                    }
                }
            }

            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);
        }
        private string SetButtonsSearch(string id)
        {
            string status;
            if (_userManager.GetUserId(User) == id)
            {
                status = "Propriu";
            }
            else
            {
                var friendships_1 = db.Friendships.Where(req => req.Requester.Id == id && req.Status == "Accepted" && req.Adresee.Id == _userManager.GetUserId(User))
                                                    .Include(req => req.Requester)
                                                    .ThenInclude(us => us.Profile)
                                                    .Include(req => req.Adresee)
                                                    .ThenInclude(us2 => us2.Profile).Count();
                var friendships_2 = db.Friendships.Where(req => req.Adresee.Id == id && req.Status == "Accepted" && req.Requester.Id == _userManager.GetUserId(User))
                                                    .Include(req => req.Requester)
                                                    .ThenInclude(us => us.Profile)
                                                    .Include(req => req.Adresee)
                                                    .ThenInclude(us2 => us2.Profile).Count();
                if (friendships_1 != 0 || friendships_2 != 0)
                {
                    status = "Prieten";
                }

                else
                {
                    var sent = db.Friendships.Where(req => req.Requester.Id == _userManager.GetUserId(User) && req.Status == "Sent" && req.Adresee.Id == id)
                                                   .Include(req => req.Requester)
                                                   .ThenInclude(us => us.Profile)
                                                   .Include(req => req.Adresee)
                                                   .ThenInclude(us2 => us2.Profile);

                    var received = db.Friendships.Where(req => req.Adresee.Id == _userManager.GetUserId(User) && req.Status == "Sent" && req.Requester.Id == id)
                                                   .Include(req => req.Requester)
                                                   .ThenInclude(us => us.Profile)
                                                   .Include(req => req.Adresee)
                                                   .ThenInclude(us2 => us2.Profile);

                    if (sent.Count() != 0)
                    {
                        status = "Trimis";
                    }


                    else if (received.Count() != 0)
                    {
                        status = "Primit"; 
                    }

                    else
                    {
                        status = "Nimic";
                    }
                }
            }
            return status;
        }
    }
}
