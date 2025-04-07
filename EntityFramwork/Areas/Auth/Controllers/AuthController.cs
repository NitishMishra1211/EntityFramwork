using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using web.JWT;


namespace web.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = await _authService.LoginAsync(model);
                if (string.IsNullOrEmpty(token))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
                TempData["JwtToken"] = token;
                // Redirect to a secure page or dashboard
                return RedirectToAction("Index", "Home", new {area=""});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");

                return View(model);
            }
        }

        // GET: /Auth/UserDetails
        //public IActionResult UserDetails()
        //{
        //    var token = TempData["JwtToken"]?.ToString();
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    var userDetails = _authService.GetUserDetailsAsync(token);
        //    return View(userDetails);
        //}


    }
}
