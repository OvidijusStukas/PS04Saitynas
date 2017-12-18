using System;
using System.Collections.Generic;
using ECoinTracker.Managers.Interfaces;
using ECoinTrackerModels.Models;
using RestSharp;

namespace ECoinTracker.Managers
{
    public class ChartManager : BaseCexManager, IChartManager
    {
	    private const string BaseUrl = "https://cex.io/api/price_stats";

	    public ChartManager() : base(BaseUrl)
	    {
	    }

	    public IEnumerable<ChartModel> GetDayChart(string symbolFrom, string symbolTo)
	    {
			var request = new RestRequest(string.Join('/', symbolFrom, symbolTo), Method.POST);
		    request.AddJsonBody(new { lastHours = "24", maxRespArrSize = 100 });

		    dynamic currencyLimits = Client.Execute<dynamic>(request);
		    return DeserializeModels(currencyLimits);
	    }

		public IEnumerable<ChartModel> GetWeekChart(string symbolFrom, string symbolTo)
	    {
			var request = new RestRequest(string.Join('/', symbolFrom, symbolTo), Method.POST);
		    request.AddJsonBody(new { lastHours = (7 * 24).ToString(), maxRespArrSize = 100 });

		    dynamic currencyLimits = Client.Execute<dynamic>(request);
		    return DeserializeModels(currencyLimits);
		}

	    public IEnumerable<ChartModel> GetMonthChart(string symbolFrom, string symbolTo)
	    {
			var request = new RestRequest(string.Join('/', symbolFrom, symbolTo), Method.POST);
		    request.AddJsonBody(new { lastHours = (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) * 24).ToString(), maxRespArrSize = 100 });

		    dynamic currencyLimits = Client.Execute<dynamic>(request);
		    return DeserializeModels(currencyLimits);
		}

	    private IEnumerable<ChartModel> DeserializeModels(dynamic response)
	    {
			// Get currency pairs:
		    List<ChartModel> chartModels = new List<ChartModel>();
		    foreach (var pair in response.Data)
		    {
			    chartModels.Add(new ChartModel
			    {
				    Timestamp = pair.tmsp.ToString(),
				    Price = pair.price.ToString()
			    });
		    }

		    return chartModels;
		}
    }
}
