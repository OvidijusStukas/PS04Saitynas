using System.Collections.Generic;
using ECoinTrackerModels.Models;

namespace ECoinTracker.Managers.Interfaces
{
    public interface ICurrencyManager
    {
	    IEnumerable<CurrencyPairModel> GetCurrencyPairs();
    }
}
