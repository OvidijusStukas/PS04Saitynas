using RestSharp;

namespace ECoinTackerFrontEnd.Managers
{
    public abstract class BaseManager
    {
	    protected readonly RestClient Client;

	    protected BaseManager(string baseUrl)
	    {
		    Client = new RestClient(baseUrl);
	    }
    }
}
