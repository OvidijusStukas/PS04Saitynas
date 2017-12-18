using ECoinTrackerModels.Requests;
using ECoinTrackerModels.Responses;

namespace ECoinTracker.Managers.Interfaces
{
    public interface IAccountManager
    {
	    LoginResponse Login(LoginRequest loginRequest);

	    bool Register(RegisterRequest registerRequest);
    }
}
