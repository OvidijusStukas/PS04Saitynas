using System.Collections.Generic;
using ECoinTrackerModels.Models;

namespace ECoinTackerFrontEnd.Managers.Interfaces
{
    public interface ICurrencyManager
    {
	    IEnumerable<CurrencyPairModel> Currencies();
    }
}
