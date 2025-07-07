using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using toDoAPP.Models;
using toDoAPP.Repository;
using System.Security.Claims;

namespace toDoAPP.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _repo;
        public UserController(UserRepository repo)
        {
            _repo = repo;
        }
        
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var model = await _repo.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
                if (model == null)
                {
                    _repo.CreateUser(new Models.User
                    {
                        UserName = user.UserName,
                        Name = user.Name,
                        Password = user.Password
                    });
                    return RedirectToAction("Index", "Task");
                }
                else
                {
                    ModelState.AddModelError("", "Username ya da İsim kullanımda.");
                }
            }
            return RedirectToAction("Index", "Task");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUser = _repo.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

                if (isUser != null)
                {
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                }
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var user = _repo
                       .Users
                       .FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

    }
    
}

