using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using $safeprojectname$.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;

namespace $safeprojectname$
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(configure =>
            {
                configure.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //configure.RegisterServicesFromAssembly(typeof(GetQueryHandler).Assembly);
				//configure.RegisterServicesFromAssembly(typeof(CreateCommand).Assembly);
				//configure.RegisterServicesFromAssembly(typeof(DeleteCommand).Assembly);
				//configure.RegisterServicesFromAssembly(typeof(UpdateCommand).Assembly);
            });

            //builder.Services.AddInfrstructureFeatures(builder.Configuration, builder.Environment);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }            
    }
}
