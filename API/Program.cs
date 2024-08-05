using API.Extensions;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
	.WithOrigins("https://medic-lab-web.azurewebsites.net", "https://localhost:4200"));

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<MedicLabContext>();
	var userManager = services.GetRequiredService<UserManager<User>>();
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	await context.Database.MigrateAsync();
	await MedicLabContextSeed.SeedUsersAsync(userManager, roleManager);
}
catch (Exception ex)
{
	Console.WriteLine(ex);
	throw;
}

app.Run();
