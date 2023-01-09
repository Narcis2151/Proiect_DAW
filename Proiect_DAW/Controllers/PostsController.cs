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
            //foreach(Post post in posts)
            //{
            //    post.PostComments = _db.Comments.Where(x => x.PostId == post.Id).OrderByDescending(x => x.CreateDate).Include("CreatorUser").Select(x => new Comment
            //    {
            //        Id = x.Id,
            //        Text = x.Text,
            //        CreateDate = x.CreateDate,
            //        CreatorUserId = x.CreatorUserId,
            //        CreatorUser = x.CreatorUser,
            //        PostId = x.PostId
            //    }).ToList();
            //}
            ViewBag.Posts = posts;
            return View();
        }

        [HttpPost]
        public IActionResult _CreatePostPartial([Bind]Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreateDate = DateTime.UtcNow;
                post.Likes = 0;
                post.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _db.Posts.Add(post);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult _AddCommentPartial([Bind]Comment comment)
        {
            if (ModelState.IsValid)
            { 
                comment.CreatorUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                comment.CreateDate = DateTime.UtcNow;
                _db.Comments.Add(comment);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
