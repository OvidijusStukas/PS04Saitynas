using ECoinTracker.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECoinTracker.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class CurrencyController : Controller
    {
	    private readonly ICurrencyManager _currencyManager;
	    public CurrencyController(ICurrencyManager currencyManager)
	    {
		    _currencyManager = currencyManager;
	    }

		[HttpGet]
	    public IActionResult Get()
		{
			return new JsonResult(_currencyManager.GetCurrencyPairs());
		}
    }
}