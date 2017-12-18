using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECoinTracker.Entities
{
    public class AccountEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AccountId { get; set; }

	    public string FullName { get; set; }

	    public string Username { get; set; }

	    public string Password { get; set; }
	}
}
