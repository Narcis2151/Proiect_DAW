using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW.Data;
using Proiect_DAW.Models;
using System.Data;
using System.Linq;

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

        [Authorize(Roles = "User,Admin")]
        public IActionResult Index(string id)
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            if (id != _userManager.GetUserId(User))
            {
                TempData["message_access"] = "You cannot make changes to another user's friends list!";
                return RedirectToAction("Show", new { id = id });

            }
            var friendships_user_1 = db.Friendships.Where(req => req.Requester.Id == id && req.Status == "Accepted")
                                        .Include(req => req.Requester)
                                        .ThenInclude(us => us.Profile)
                                        .Include(req => req.Adresee)
                                        .ThenInclude(us2 => us2.Profile);

            var friendships_user_2 = db.Friendships.Where(req => req.Adresee.Id == id && req.Status == "Accepted")
                                        .Include(req => req.Requester)
                                        .ThenInclude(us => us.Profile)
                                        .Include(req => req.Adresee)
                                        .ThenInclude(us2 => us2.Profile);
            

            List<Profile> user_friends = new List<Profile>();
            foreach (Friendship friendship in friendships_user_1)
            {
                user_friends.Add(friendship.Adresee.Profile);
            }

            foreach (Friendship friendship in friendships_user_2)
            {
                user_friends.Add(friendship.Requester.Profile);
            }


            var sent_user = db.Friendships.Where(req => req.Requester.Id == id && req.Status == "Sent")
                                        .Include(req => req.Requester)
                                        .ThenInclude(us => us.Profile)
                                        .Include(req => req.Adresee)
                                        .ThenInclude(us2 => us2.Profile);

            List<Profile> user_sent_friends = new List<Profile>();
            foreach (Friendship friendship in sent_user)
            {
                user_sent_friends.Add(friendship.Adresee.Profile);
            }

            var received_user = db.Friendships.Where(req => req.Adresee.Id == id && req.Status == "Sent")
                                        .Include(req => req.Requester)
                                        .ThenInclude(us => us.Profile)
                                        .Include(req => req.Adresee)
                                        .ThenInclude(us2 => us2.Profile);
            List<Profile> user_pending_friends = new List<Profile>();
            foreach (Friendship friendship in received_user)
            {
                user_pending_friends.Add(friendship.Requester.Profile);
            }

            ViewBag.Friends = user_friends;
            ViewBag.PendingFriends = user_pending_friends;
            ViewBag.SentFriends = user_sent_friends;
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Show(string id)
        {
            if (TempData.ContainsKey("message_access"))
            {
                ViewBag.Message = TempData["message_access"];
            }
            Profile profile = db.Profiles.Where(prof => prof.ApplicationUserId == id)
                                      .Include(prof => prof.ApplicationUser)
                                      .ThenInclude(us => us.Posts)
                                      .FirstOrDefault();

            var friendships_user_1 = db.Friendships.Where(req => req.Requester.Id == id && req.Status == "Accepted" && req.Adresee.Id != _userManager.GetUserId(User))
                                        .Include(req => req.Requester)
                                        .ThenInclude(us => us.Profile)
                                        .Include(req => req.Adresee)
                                        .ThenInclude(us2 => us2.Profile);

            var friendships_user_2 = db.Friendships.Where(req => req.Adresee.Id == id && req.Status == "Accepted" && req.Requester.Id != _userManager.GetUserId(User))
                                        .Include(req => req.Requester)
                                        .ThenInclude(us => us.Profile)
                                        .Include(req => req.Adresee)
                                        .ThenInclude(us2 => us2.Profile);
            List<Profile> user_friends = new List<Profile>();
            foreach(Friendship friendship in friendships_user_1)
            {
                user_friends.Add(friendship.Adresee.Profile);
            }

            foreach (Friendship friendship in friendships_user_2)
            {
                user_friends.Add(friendship.Requester.Profile);
            }

            List<Profile> user_pending_friends = new List<Profile>();
            foreach (Profile friend in user_friends)
            {
                var nr = db.Friendships.Where(req => req.Requester.Id == friend.ApplicationUserId &&
                                              req.Status == "Sent" &&
                                              req.Adresee.Id == _userManager.GetUserId(User)).Count();
                if (nr > 0)
                {
                    user_pending_friends.Add(friend);
                }
            }

            List<Profile> user_sent_friends = new List<Profile>();
            foreach (Profile friend in user_friends)
            {
                var nr = db.Friendships.Where(req => req.Adresee.Id == friend.ApplicationUserId &&
                                              req.Status == "Sent" &&
                                              req.Requester.Id == _userManager.GetUserId(User)).Count();
                if (nr > 0)
                {
                    user_sent_friends.Add(friend);
                }
            }

            List<Profile> user_common_friends = new List<Profile>();
            foreach (Profile friend in user_friends)
            {
                var nr = db.Friendships.Where(req => req.Adresee.Id == friend.ApplicationUserId &&
                                              req.Status == "Accepted" &&
                                              req.Requester.Id == _userManager.GetUserId(User)).Count();
                if (nr > 0)
                {
                    user_common_friends.Add(friend);
                }
            }
            foreach (Profile friend in user_friends)
            {
                var nr = db.Friendships.Where(req => req.Requester.Id == friend.ApplicationUserId &&
                                              req.Status == "Accepted" &&
                                              req.Adresee.Id == _userManager.GetUserId(User)).Count();
                if (nr > 0)
                {
                    user_common_friends.Add(friend);
                }
            }
            foreach (Profile friend in user_common_friends)
            {
                user_friends.Remove(friend);
            }
            foreach (Profile friend in user_pending_friends)
            {
                user_friends.Remove(friend);
            }
            foreach (Profile friend in user_sent_friends)
            {
                user_friends.Remove(friend);
            }

            ViewBag.Friends = user_friends;
            ViewBag.PendingFriends = user_pending_friends;
            ViewBag.SentFriends = user_sent_friends;
            ViewBag.CommonFriends = user_common_friends;
            return View(profile);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult SetFriendship(string status, string id)
        {
            if (status == "Send" && ModelState.IsValid)
            {
                Friendship friendship = new Friendship();
                friendship.Status = "Sent";
                friendship.Requester = db.ApplicationUsers.Find(_userManager.GetUserId(User));
                friendship.Adresee = db.ApplicationUsers.Find(id);
                db.Friendships.Add(friendship);
                db.SaveChanges();
                TempData["message"] = "Friendship request sent successfully";
            }

            if (status == "Accept" && ModelState.IsValid)
            {
                Friendship friendship = db.Friendships.Where(req => req.Requester.Id == id &&
                                                             req.Status == "Sent" &&
                                                             req.Adresee.Id == _userManager.GetUserId(User)
                                                             ).First();
                friendship.Status = "Accepted";
                db.SaveChanges();
                TempData["message"] = "Friendship request accepted successfully";
            }

            if (status == "Decline" && ModelState.IsValid)
            {
                Friendship friendship = db.Friendships.Where(req => req.Requester.Id == id &&
                                                             req.Status == "Sent" &&
                                                             req.Adresee.Id == _userManager.GetUserId(User)
                                                             ).First();
                db.Friendships.Remove(friendship);
                db.SaveChanges();
                TempData["message"] = "Friendship request declined successfully";
            }

            if (status == "Unsend" && ModelState.IsValid)
            {
                Friendship friendship = db.Friendships.Where(req => req.Requester.Id == _userManager.GetUserId(User) &&
                                                             req.Status == "Sent" &&
                                                             req.Adresee.Id == id
                                                             ).First();
                db.Friendships.Remove(friendship);
                db.SaveChanges();
                TempData["message"] = "Friendship request unsent successfully";
            }

            if (status == "Remove" && ModelState.IsValid)
            {
                int nr1 = db.Friendships.Where(req => req.Requester.Id == _userManager.GetUserId(User) &&
                                                             req.Status == "Accepted" &&
                                                             req.Adresee.Id == id
                                                             ).Count();
                int nr2 = db.Friendships.Where(req => req.Requester.Id == id &&
                                                             req.Status == "Accepted" &&
                                                             req.Adresee.Id == _userManager.GetUserId(User)
                                                             ).Count();
                Friendship friendship = new Friendship();

                if (nr1 == 1)
                {
                    friendship = db.Friendships.Where(req => req.Requester.Id == _userManager.GetUserId(User) &&
                                                             req.Status == "Accepted" &&
                                                             req.Adresee.Id == id
                                                             ).First();
                }
                else if (nr2 == 1)
                {
                    friendship = db.Friendships.Where(req => req.Requester.Id == id &&
                                                             req.Status == "Accepted" &&
                                                             req.Adresee.Id == _userManager.GetUserId(User)
                                                             ).First();
                }
                db.Friendships.Remove(friendship);
                db.SaveChanges();
                TempData["message"] = "Friend removed successfully";
            }


            return RedirectToAction("Index", new { id = _userManager.GetUserId(User) });
        }

        
    }
}
