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
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            string allowedOrigins = config.GetValue<string>("AllowedOrigins")!;
			services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigins",
					builder =>
					{
						builder
							.WithOrigins(allowedOrigins) 
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials(); 
					});
			});

			return services;
        }
    }
}