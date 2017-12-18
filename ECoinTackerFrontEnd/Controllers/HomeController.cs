using ECoinTackerFrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECoinTackerFrontEnd.Controllers
{
    public class HomeController : Controller
    {
	    private readonly ICurrencyManager _currencyManager;

	    public HomeController(ICurrencyManager currencyManager)
	    {
		    _currencyManager = currencyManager;
	    }

		[HttpGet]
        public IActionResult Index()
        {
            return View(_currencyManager.Currencies());
        }

    }
}
