using System.Net;
using ECoinTackerFrontEnd.Configuration;
using ECoinTackerFrontEnd.Managers.Interfaces;
using ECoinTrackerModels.Requests;
using ECoinTrackerModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace ECoinTackerFrontEnd.Managers
{
    public class AccountManager : BaseManager, IAccountManager
    {
	    private readonly IHttpContextAccessor _contextAccessor;

	    public AccountManager(IHttpContextAccessor contextAccessor, IOptions<ApplicationConfiguration> configuration) 
			: base(configuration.Value.ApiEndPoint + "account")
	    {
		    _contextAccessor = contextAccessor;
	    }

	    public bool Login(string username, string password)
	    {
		    var request = new RestRequest("login", Method.POST);
		    request.AddJsonBody(new LoginRequest
		    {
			    Username = username,
				Password = password
		    });

		    IRestResponse<LoginResponse> response = Client.Execute<LoginResponse>(request);
		    if (response.StatusCode == HttpStatusCode.OK && response.Data?.Token != null)
		    {
			    _contextAccessor.HttpContext.Session.SetString("token", JsonConvert.SerializeObject(response.Data.Token));
			    _contextAccessor.HttpContext.Session.SetString("account_fullname", response.Data.AccountFullname);
			    _contextAccessor.HttpContext.Session.SetInt32("account_id", response.Data.AccountId);
				return true;
			}

		    return false;
	    }

	    public bool Register(string fullname, string username, string password)
	    {
			var request = new RestRequest("register", Method.POST);
		    request.AddJsonBody(new RegisterRequest
		    {
				FullName = fullname,
			    Username = username,
			    Password = password
		    });

		    IRestResponse response = Client.Execute(request);
		    return response.StatusCode == HttpStatusCode.OK;
	    }

	    public void Logout()
	    {
			_contextAccessor.HttpContext.Session.Remove("token");
			_contextAccessor.HttpContext.Session.Remove("account_fullname");
			_contextAccessor.HttpContext.Session.Remove("account_id");
		}
    }
}
