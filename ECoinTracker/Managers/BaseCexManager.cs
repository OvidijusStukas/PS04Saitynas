using RestSharp;
using RestSharp.Deserializers;

namespace ECoinTracker.Managers
{
    public abstract class BaseCexManager
    {
	    protected readonly RestClient Client;

	    protected BaseCexManager(string baseUrl)
	    {
		    Client = new RestClient(baseUrl);
			Client.AddHandler("text/json", new DynamicJsonDeserializer());
			Client.AddHandler("application/json", new DynamicJsonDeserializer());
		}
    }
}
