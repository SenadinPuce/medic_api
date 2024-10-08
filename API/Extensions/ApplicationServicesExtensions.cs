using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;


namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
			services.AddControllers();

			services.AddDbContext<MedicLabContext>(options =>
				options.UseSqlServer(config.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure(
					maxRetryCount: 10,
					maxRetryDelay: TimeSpan.FromSeconds(30),
					errorNumbersToAdd: null);
				}));


			services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddCors();

			return services;
        }
    }
}