using System.Collections.Generic;
using ECoinTracker.Enums;
using ECoinTracker.Managers.Interfaces;
using ECoinTrackerModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECoinTracker.Controllers
{
	[Route("api/v1/[controller]")]
	[Produces("application/json")]
	public class ChartController : Controller
	{
		private readonly IChartManager _chartManager;

		public ChartController(IChartManager chartManager)
		{
			_chartManager = chartManager;
		}

		[HttpGet]
		[Route("{chartType}/{symbolFrom}/{symbolTo}")]
		public IActionResult Get(ChartType chartType, string symbolFrom, string symbolTo)
		{
			IEnumerable<ChartModel> chartModels;

			switch (chartType)
			{
				case ChartType.Day:
					chartModels = _chartManager.GetDayChart(symbolFrom, symbolTo);
					break;
				case ChartType.Week:
					chartModels = _chartManager.GetWeekChart(symbolFrom, symbolTo);
					break;
				case ChartType.Month:
					chartModels = _chartManager.GetMonthChart(symbolFrom, symbolTo);
					break;
				default:
					return BadRequest($"Chart type {chartType} is not yet supported.");
			}

			return Ok(chartModels);
		}
    }
}
