using System.Collections.Generic;
using System.Linq;
using ECoinTracker.Managers.Interfaces;
using ECoinTrackerModels.Models;
using RestSharp;

namespace ECoinTracker.Managers
{
    public class CurrencyManager : BaseCexManager, ICurrencyManager
    {
	    private const string BaseUrl = "https://cex.io/api";

	    public CurrencyManager() 
			: base(BaseUrl)
	    {
	    }

	    public IEnumerable<CurrencyPairModel> GetCurrencyPairs()
	    {
			var request = new RestRequest("currency_limits", Method.GET);

		    dynamic currencyLimits = Client.Execute<dynamic>(request);

			// Get currency pairs:
			List<CurrencyPairModel> currencyPairModels = new List<CurrencyPairModel>();
		    foreach (var pair in currencyLimits.Data.data.pairs)
		    {
			    currencyPairModels.Add(new CurrencyPairModel
			    {
					SymbolFrom = pair.symbol1.ToString(),
					SymbolTo = pair.symbol2.ToString()
			    });
		    }

			// Get last prices
		    List<string> uniqueSymbols = new List<string> { "tickers" };

		    foreach (var currencyPair in currencyPairModels)
		    {
			    if (!uniqueSymbols.Contains(currencyPair.SymbolFrom))
					uniqueSymbols.Add(currencyPair.SymbolFrom);

				if (!uniqueSymbols.Contains(currencyPair.SymbolTo))
					uniqueSymbols.Add(currencyPair.SymbolTo);
		    }

			request = new RestRequest(string.Join("/", uniqueSymbols), Method.GET);
		    dynamic lastPrices = Client.Execute<dynamic>(request);

		    foreach (var dataObj in lastPrices.Data.data)
		    {
			    string currencyPair = dataObj.pair.ToString();
			    var symbolFrom = currencyPair.Split(':')[0];
			    var symbolTo = currencyPair.Split(':')[1];

				var matchingPair = currencyPairModels.FirstOrDefault(mPair => mPair.SymbolFrom == symbolFrom && mPair.SymbolTo == symbolTo);
			    if (matchingPair == null)
				    continue;

			    matchingPair.LastPrice = dataObj.last.ToString();
		    }

		    return currencyPairModels.OrderBy(pair => pair.SymbolFrom);
	    }
    }
}
