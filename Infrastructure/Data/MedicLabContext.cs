using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class MedicLabContext : IdentityDbContext<User, Role, int,
		IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		public MedicLabContext(DbContextOptions<MedicLabContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Ignore<IdentityUserClaim<int>>();
			builder.Ignore<IdentityUserLogin<int>>();
			builder.Ignore<IdentityUserToken<int>>();
			builder.Ignore<IdentityRoleClaim<int>>();


			builder.Entity<User>()
		  		.HasMany(ur => ur.UserRoles)
		  		.WithOne(u => u.User)
		  		.HasForeignKey(ur => ur.UserId)
		  		.IsRequired();

			builder.Entity<Role>()
				.HasMany(ur => ur.UserRoles)
				.WithOne(u => u.Role)
				.HasForeignKey(ur => ur.RoleId)
				.IsRequired();
		}
	}
}
