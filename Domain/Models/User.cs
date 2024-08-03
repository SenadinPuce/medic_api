using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; } = null!;
		public DateTime LastLoginDate { get; set; }
		public int Orders { get; set; }
		public string ImageUrl { get; set; } = null!;
		public DateTime DateOfBirth { get; set; }
		public bool IsBlocked { get; set; }
	}
}
