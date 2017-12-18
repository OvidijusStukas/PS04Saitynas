using System.Net;
using ECoinTracker.Managers.Interfaces;
using ECoinTrackerModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECoinTracker.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
	    private readonly IAccountManager _accountManager;

	    public AccountController(IAccountManager accountManager)
	    {
		    _accountManager = accountManager;
	    }

		[HttpPost]
		[AllowAnonymous]
		[Route("login")]
	    public IActionResult Login([FromBody] LoginRequest loginRequest)
	    {
		    if (string.IsNullOrWhiteSpace(loginRequest?.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
		    {
			    return BadRequest();
		    }

		    var response = _accountManager.Login(loginRequest);
		    if (response == null)
			    return Unauthorized();

			return Ok(response);
	    }

		[HttpPost]
		[AllowAnonymous]
		[Route("register")]
	    public IActionResult Register([FromBody] RegisterRequest registerRequest)
	    {
			if (string.IsNullOrWhiteSpace(registerRequest?.FullName) || string.IsNullOrWhiteSpace(registerRequest.Username) || string.IsNullOrWhiteSpace(registerRequest.Password))
			{
				return BadRequest();
			}

		    bool success = _accountManager.Register(registerRequest);
		    return success ? Ok() : new StatusCodeResult((int) HttpStatusCode.Conflict);
	    }
    }
}