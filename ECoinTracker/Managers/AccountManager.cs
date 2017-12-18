using System.Linq;
using ECoinTracker.Configurations;
using ECoinTracker.Contexts;
using ECoinTracker.Entities;
using ECoinTracker.Managers.Interfaces;
using ECoinTrackerModels.Jwt;
using ECoinTrackerModels.Requests;
using ECoinTrackerModels.Responses;
using Microsoft.Extensions.Options;

namespace ECoinTracker.Managers
{
    public class AccountManager : IAccountManager
    {
	    private readonly IOptions<ApplicationConfiguration> _applicationConfiguration;
	    private readonly AccountDbContext _accountDbContext;

		public AccountManager(IOptions<ApplicationConfiguration> applicationConfiguration, AccountDbContext accountDbContext)
		{
			_applicationConfiguration = applicationConfiguration;
			_accountDbContext = accountDbContext;

		}

	    public LoginResponse Login(LoginRequest loginRequest)
	    {
			AccountEntity accountEntity = _accountDbContext.Accounts.FirstOrDefault(acc => acc.Username == loginRequest.Username);
		    if (accountEntity == null)
			    return null;

		    if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, accountEntity.Password))
			    return null;

		    var token = new JwtTokenBuilder()
			    .AddSecurityKey(JwtSecurityKey.Create(_applicationConfiguration.Value.JwtTokenSecretKey))
			    .AddSubject(accountEntity.FullName)
			    .AddIssuer(_applicationConfiguration.Value.IssuerName)
			    .AddAudience(_applicationConfiguration.Value.AudienceName)
			    .AddClaim(nameof(accountEntity.AccountId), accountEntity.AccountId.ToString())
			    .AddExpiry(30)
			    .Build();

			return new LoginResponse
			{
				Token = token,
				AccountFullname = accountEntity.FullName,
				AccountId = accountEntity.AccountId
			};
		}

	    public bool Register(RegisterRequest registerRequest)
	    {
		    var accountEntity = new AccountEntity
		    {
			    FullName = registerRequest.FullName,
			    Username = registerRequest.Username,
			    Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password)
		    };

		    if (_accountDbContext.Accounts.Any(acc => acc.Username == accountEntity.Username))
			    return false;

		    _accountDbContext.Accounts.Add(accountEntity);
		    _accountDbContext.SaveChanges();

		    return true;
	    }
    }
}
