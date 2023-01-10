using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW.Data;
using Proiect_DAW.Models;
using System.Security.Claims;

namespace Proiect_DAW.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public PostsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var posts = _db.Posts.OrderByDescending(x => x.CreateDate).Include("ApplicationUser").Include(p => p.Comments).ThenInclude(x => x.CreatorUser);
            ViewBag.Posts = posts;
            ViewBag.CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult EditPost(int id)
        {
            var text = (string)Request.Form["text"];
            if(text != null)
            {
                var post = _db.Posts.SingleOrDefault(x => x.Id == id);
                if(post != null)
                {
                    post.Text = text;
                    _db.Posts.Update(post);
                }
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult EditComment(int id)
        {
            var text = (string)Request.Form["text"];
            if(text != null)
            {
                var comment = _db.Comments.SingleOrDefault(x => x.Id == id);
                if(comment != null)
                {
                    comment.Text = text;
                    _db.Comments.Update(comment);
                }
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult _CreatePostPartial([Bind]Post post)
        {
            if (ModelState.IsValid)
            {
                var sanitizer = new HtmlSanitizer();
                post.Text = sanitizer.Sanitize(post.Text);
                post.CreateDate = DateTime.UtcNow;
                post.Likes = 0;
                post.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _db.Posts.Add(post);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult _AddCommentPartial([Bind]Comment comment)
        {
            if (ModelState.IsValid)
            {
                var sanitizer = new HtmlSanitizer();
                comment.Text = sanitizer.Sanitize(comment.Text);
                comment.CreatorUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                comment.CreateDate = DateTime.UtcNow;
                _db.Comments.Add(comment);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult DeletePost(int id)
        {
            var comments = _db.Comments.Where(x => x.PostId == id);
            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    _db.Comments.Remove(comment);
                }
            }
            var post = _db.Posts.SingleOrDefault(x => x.Id == id);
            if (post != null)
            {
                _db.Posts.Remove(post);
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _db.Comments.SingleOrDefault(x => x.Id == id);
            if(comment != null)
            {
                _db.Comments.Remove(comment);
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
