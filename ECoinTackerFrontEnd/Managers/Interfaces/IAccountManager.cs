namespace ECoinTackerFrontEnd.Managers.Interfaces
{
    public interface IAccountManager
    {
	    bool Login(string username, string password);

	    bool Register(string fullname, string username, string password);

	    void Logout();
    }
}
