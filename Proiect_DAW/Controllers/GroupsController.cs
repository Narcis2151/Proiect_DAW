using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW.Data;
using Proiect_DAW.Models;

namespace Proiect_DAW.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public GroupsController(
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
        public IActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            ViewBag.Groups = GetGroups();
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Show(int id)
        {
            var is_in_group = db.ApplicationUserGroups.Where(aug => aug.ApplicationUserId == _userManager.GetUserId(User) && aug.GroupId == id).Count();
            if (is_in_group == 0)
            {
                TempData["message"] = "You are not a member of this group!";
                return RedirectToAction("Index");
            }
            var grup = db.Groups.Where(grup => grup.Id == id).Include(grup => grup.Message).ThenInclude(msg => msg.Sender).ThenInclude(usr => usr.Profile);
            List<ApplicationUser> users = (from ug in db.ApplicationUserGroups
                                    where ug.GroupId == id
                                    select ug.ApplicationUser).ToList();

            if (grup.Count() == 0)
            {
                TempData["message"] = "This group does not exist!";
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Group = grup.First();
                ViewBag.User = _userManager.GetUserId(User);
                ViewBag.Users = users;
                ViewBag.Messages = grup.First().Message.OrderBy(msg => msg.DateCreated);
                ViewBag.Groups = GetGroups();
            }
            return View(grup.First());
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public IActionResult Show(int id, [FromForm] Message message)
        {
            var is_in_group = db.ApplicationUserGroups.Where(aug => aug.ApplicationUserId == _userManager.GetUserId(User) && aug.GroupId == id).Count();
            if (is_in_group == 0)
            {
                TempData["message"] = "You are not a member of this group!";
                return RedirectToAction("Index");
            }
            message.Sender = db.ApplicationUsers.Find(_userManager.GetUserId(User));
            message.DateCreated = DateTime.Now;
            message.Group = db.Groups.Find(id);

            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return Redirect("/Groups/Show/" + message.Group.Id);
            }

            else
            {
                var grup = db.Groups.Where(grup => grup.Id == id).Include(grup => grup.Message).ThenInclude(msg => msg.Sender).ThenInclude(usr => usr.Profile);
                List<ApplicationUser> users = (from ug in db.ApplicationUserGroups
                                               where ug.GroupId == id
                                               select ug.ApplicationUser).ToList();

                
                ViewBag.Group = grup.First();
                ViewBag.User = _userManager.GetUserId(User);
                ViewBag.Users = users;
                ViewBag.Messages = grup.First().Message.OrderBy(msg => msg.DateCreated);
                ViewBag.Groups = GetGroups();
                
                return View(grup.First());
            }
        }
        public IActionResult New()
        {
            var has_profile = db.Profiles.Find(_userManager.GetUserId(User));
            if(has_profile == null)
            {
                return RedirectToAction("/Profiles/Show/" + _userManager.GetUserId(User));
            }
            Group grup = new Group();
            var fr1 = db.Friendships.Where(fr => fr.Requester.Id == _userManager.GetUserId(User) && fr.Status == "Accepted");
            var fr2 = db.Friendships.Where(fr => fr.Adresee.Id == _userManager.GetUserId(User) && fr.Status == "Accepted");
            List<Profile> profiles = new List<Profile>();
            foreach(Friendship fr in fr1)
            {
                Profile prof = db.Profiles.Find(fr.Adresee.Id);
                if (prof != null)
                {
                    profiles.Add(prof);
                }
            }
            foreach (Friendship fr in fr2)
            {
                Profile prof = db.Profiles.Find(fr.Requester.Id);
                if (prof != null)
                {
                    profiles.Add(prof);
                }
            }

            if (profiles.Count() == 0)
            {
                TempData["message"] = "You cannot create a group without having any friends!";
                return RedirectToAction("Index");

            }
            else
            {
                List <SelectListItem> selectList = new List<SelectListItem>();
                foreach(var prof in profiles)
                {
                    selectList.Add(new SelectListItem{ Value = (prof.ApplicationUserId), Text = (prof.FirstName + " " + prof.LastName)});
                }
                ViewBag.List = selectList;
                ViewBag.Groups = GetGroups();
                
            }
            return View(grup);
        }

        private List<Group> GetGroups() 
        { 
            List<Group> groups = new List<Group>();
            var group_ids = from ug in db.ApplicationUserGroups
                            where ug.ApplicationUserId == _userManager.GetUserId(User)
                            select ug.GroupId;

            
            foreach (var group_id in group_ids) 
            {
                Group group = db.Groups.Find(group_id);
                groups.Add(group);
            }
            return groups;
        }

        private <
    }
}
