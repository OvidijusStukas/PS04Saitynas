using System.Collections.Generic;
using ECoinTrackerModels.Models;

namespace ECoinTackerFrontEnd.Managers.Interfaces
{
    public interface IChartManager
    {
	    IEnumerable<ChartModel> GetDayChart(string symbolFrom, string symbolTo);

	    IEnumerable<ChartModel> GetWeekChart(string symbolFrom, string symbolTo);

	    IEnumerable<ChartModel> GetMonthChart(string symbolFrom, string symbolTo);
    }
}
