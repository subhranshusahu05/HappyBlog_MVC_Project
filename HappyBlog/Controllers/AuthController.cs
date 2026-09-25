using HappyBlog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyBlog.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //check for validation
            if (ModelState.IsValid)
            {
                //create identity user object
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                //user create
                var result = await _userManager.CreateAsync(user, model.Password);

                //if user create successfully
                if (result.Succeeded)
                {
                    //if the user role exist in the database 
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {

                        await _roleManager.CreateAsync(new IdentityRole("User"));

                    }

                    await _userManager.AddToRoleAsync(user, "User");

                    await _signManager.SignInAsync(user, isPersistent: true);

                    //return RedirectToAction("Index","Home");
                    return RedirectToAction("Index", "Post");
                }

            }
            return View(model);
        }


        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signManager;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signManager = signInManager;
        }







        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");

                    return View(model);

                }

                var signInresult = await _signManager.PasswordSignInAsync(user, model.Password, false, false);

                if (!signInresult.Succeeded)
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                    return View(model);
                }


                return RedirectToAction("Index", "Post");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Post");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }






    }
}
