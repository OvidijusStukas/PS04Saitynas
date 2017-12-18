using System.Collections.Generic;
using System.Net;
using ECoinTackerFrontEnd.Configuration;
using ECoinTackerFrontEnd.Managers.Interfaces;
using ECoinTrackerModels.Models;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ECoinTackerFrontEnd.Managers
{
    public class ChartManager : BaseManager, IChartManager
    {
	    public ChartManager(IOptions<ApplicationConfiguration> configuration) 
			: base(configuration.Value.ApiEndPoint + "chart")
	    {
	    }

	    public IEnumerable<ChartModel> GetDayChart(string symbolFrom, string symbolTo)
	    {
		    string query = string.Join('/', '0', symbolFrom, symbolTo);

		    var request = new RestRequest(query, Method.GET);

		    IRestResponse<List<ChartModel>> response = Client.Execute<List<ChartModel>>(request);
		    return response.StatusCode == HttpStatusCode.OK ? response.Data : null;
	    }

	    public IEnumerable<ChartModel> GetWeekChart(string symbolFrom, string symbolTo)
	    {
			string query = string.Join('/', '1', symbolFrom, symbolTo);

		    var request = new RestRequest(query, Method.GET);

		    IRestResponse<List<ChartModel>> response = Client.Execute<List<ChartModel>>(request);
		    return response.StatusCode == HttpStatusCode.OK ? response.Data : null;
		}

	    public IEnumerable<ChartModel> GetMonthChart(string symbolFrom, string symbolTo)
	    {
			string query = string.Join('/', '2', symbolFrom, symbolTo);

		    var request = new RestRequest(query, Method.GET);

		    IRestResponse<List<ChartModel>> response = Client.Execute<List<ChartModel>>(request);
		    return response.StatusCode == HttpStatusCode.OK ? response.Data : null;
		}
    }
}
