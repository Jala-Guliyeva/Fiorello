using FiorelloTask.Models;
using FiorelloTask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace FiorelloTask.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task< IActionResult> Register(RegisterVM register)
        {
            if(!ModelState.IsValid) return View();

            AppUser newUser = new AppUser()
            {
                Fullname = register.FullName,
                UserName = register.UserName,
                Email = register.Email
            };

            IdentityResult result =await _userManager.CreateAsync(newUser, 
                register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                    
                }
                return View(register);
            }

            await _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("index","home");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();

            AppUser dbUser=await _userManager.FindByEmailAsync(login.Email);
            if (dbUser==null)
            {
                ModelState.AddModelError("", "email or password wrong");
                return View(login);
            }

       
            SignInResult result = await _signInManager.PasswordSignInAsync(dbUser, login.Password,
               login.RememberMe , true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "your account is lockout");
                return View(login);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "email or password wrong");
                return View(login);

            }
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
