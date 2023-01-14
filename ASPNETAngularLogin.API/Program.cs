using ASPNETAngularLogin.Data;
using ASPNETAngularLogin.Data.Entities;
using ASPNETAngularLogin.Data.Repositories.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASPNETAngularLogin.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddIdentity<User, IdentityRole<int>>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<ASPNETAngularLoginDbContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication();

            string connectionString = builder.Configuration.GetConnectionString("ASPLoginCS");
            builder.Services.AddDbContext<ASPNETAngularLoginDbContext>(
                options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}