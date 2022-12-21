#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW.Data;
using Proiect_DAW.Models;
using System.ComponentModel.DataAnnotations;

namespace Proiect_DAW.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public string Username { get; set; }


        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Display(Name = "Username")]
            public string Username { get; set; }
            [DataType(DataType.DateTime)]
            [Display(Name = "Birthdate")]
            public DateTime ProfileBirthdate { get; set; }
            [Display(Name = "Profile Description")]
            public string Description { get; set; }
            [Display(Name = "Is Private?")]
            public bool IsPrivate { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userID = await _userManager.GetUserIdAsync(user);
            var firstName = "";
            var lastName = "";
            var description = "";
            bool isPrivate = false;
            var username = "";

            int numar = db.Profiles.Include("ApplicationUser").Where(prof => prof.ApplicationUserId == userID).Count();
            if (numar != 0)
            {
                Profile profile = db.Profiles.Include("ApplicationUser")
                                          .Where(prof => prof.ApplicationUserId == _userManager.GetUserId(User))
                                          .First();
                firstName = profile.FirstName;
                lastName = profile.LastName;
                description = profile.Description;
                isPrivate = profile.IsPrivate;
                username = profile.Username;

            }


            Input = new InputModel
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                IsPrivate = isPrivate,
                Description = description,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            int numar = db.Profiles.Include("ApplicationUser").Where(prof => prof.ApplicationUserId == _userManager.GetUserId(User)).Count();
            Profile profile;
            if (numar == 0)
            {
                profile = new Profile();
                ViewData["is_new_profile"] = "yes";
            }
            else
            {
                ViewData["is_new_profile"] = "no";
                profile = db.Profiles.Include("ApplicationUser")
                                          .Where(prof => prof.ApplicationUserId == _userManager.GetUserId(User))
                                          .First();

            }

            var firstName = profile.FirstName;
            var lastName = profile.LastName;
            var description = profile.Description;
            bool isPrivate = profile.IsPrivate;
            var username = profile.Username;

            profile.FirstName= Input.FirstName;
            profile.LastName= Input.LastName; 
            profile.Description= Input.Description;   
            profile.IsPrivate= Input.IsPrivate;
            profile.Username= Input.Username;
            profile.ApplicationUserId = user.Id;

            if(numar == 0)
            {
                db.Profiles.Add(profile);
            }

            db.SaveChanges();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
