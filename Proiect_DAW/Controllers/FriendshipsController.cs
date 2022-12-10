using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW.Data;
using Proiect_DAW.Models;

namespace Proiect_DAW.Controllers
{
    public class FriendshipsController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public FriendshipsController(
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
            var friendships = db.Friendships.Include("Requester").Include("Adresee");
            ViewBag.Frienships = friendships;
            return View();
        }
    }
}
