using ECoinTackerFrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECoinTackerFrontEnd.Controllers
{
    public class ChartController : Controller
    {
	    private readonly IChartManager _manager;

	    public ChartController(IChartManager manager)
	    {
		    _manager = manager;
	    }

		[HttpGet]
	    public IActionResult GetDayChart([FromQuery] string symbolFrom, [FromQuery] string symbolTo)
		{
			ViewBag.Name = $"{symbolFrom} / {symbolTo}";
			ViewBag.SymbolFrom = symbolFrom;
			ViewBag.SymbolTo = symbolTo;

		    return View("Chart", _manager.GetDayChart(symbolFrom, symbolTo));
	    }

		[HttpGet]
	    public IActionResult GetWeekChart([FromQuery] string symbolFrom, [FromQuery] string symbolTo)
	    {
		    ViewBag.Name = $"{symbolFrom} / {symbolTo}";
		    ViewBag.SymbolFrom = symbolFrom;
		    ViewBag.SymbolTo = symbolTo;

			return View("Chart", _manager.GetWeekChart(symbolFrom, symbolTo));
	    }

		[HttpGet]
        public IActionResult GetMonthChart([FromQuery] string symbolFrom, [FromQuery] string symbolTo)
		{
			ViewBag.Name = $"{symbolFrom} / {symbolTo}";
			ViewBag.SymbolFrom = symbolFrom;
			ViewBag.SymbolTo = symbolTo;

			return View("Chart", _manager.GetMonthChart(symbolFrom, symbolTo));
		}
    }
}