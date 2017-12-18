using System.Collections.Generic;
using ECoinTackerFrontEnd.Configuration;
using ECoinTackerFrontEnd.Managers.Interfaces;
using ECoinTrackerModels.Models;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ECoinTackerFrontEnd.Managers
{
    public class CurrencyManager : BaseManager, ICurrencyManager
    {
		public CurrencyManager(IOptions<ApplicationConfiguration> configurations) 
			: base(configurations.Value.ApiEndPoint + "currency")
	    {
	    }

	    public IEnumerable<CurrencyPairModel> Currencies()
	    {
		    var request = new RestRequest(Method.GET);

		    return Client.Execute<List<CurrencyPairModel>>(request).Data;
	    }
    }
}
