using ECoinTackerFrontEnd.Managers.Interfaces;
using ECoinTrackerModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ECoinTackerFrontEnd.Controllers
{
    public class AccountController : Controller
    {
	    private readonly IAccountManager _accountManager;

	    public AccountController(IAccountManager accountManager)
	    {
		    _accountManager = accountManager;
	    }

		[HttpGet]
        public IActionResult Login(bool? error = false)
		{
			ViewBag.Error = error;
            return View();
        }

	    [HttpPost]
	    public IActionResult Login([FromForm] LoginRequest loginRequest)
	    {
		    bool success = _accountManager.Login(loginRequest.Username, loginRequest.Password);
		    return success ? RedirectToAction("Index", "Home") : RedirectToAction("Login", "Account", new { error = true });
	    }

	    [HttpGet]
	    public IActionResult Register(bool? error = false)
	    {
		    ViewBag.Error = error;
			return View();
		}

		[HttpPost]
	    public IActionResult Register([FromForm] RegisterRequest registerRequest)
	    {
			bool success = _accountManager.Register(registerRequest.FullName, registerRequest.Username, registerRequest.Password);
		    return success ? RedirectToAction("Login") : RedirectToAction("Register", new { error = true });
	    }

		[HttpGet]
	    public IActionResult Logout()
	    {
			_accountManager.Logout();
		    return RedirectToAction("Index", "Home");
	    }
    }
}