using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
	public class Role : IdentityRole<int>
	{
		public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
	}
}
