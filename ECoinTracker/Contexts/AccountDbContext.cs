using ECoinTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECoinTracker.Contexts
{
    public class AccountDbContext : DbContext
    {
	    public AccountDbContext(DbContextOptions<AccountDbContext> options) 
			: base(options)
	    {
	    }

	    public DbSet<AccountEntity> Accounts { get; set; }
	}
}
