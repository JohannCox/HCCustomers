using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HCCustomers.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace HCCustomers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddDbContext<CustomerContext>(options => options.UseSqlite(Configuration.GetConnectionString("DBConn")) );

         services.AddMvc();

         var dbConnection = Configuration.GetConnectionString("DBConn");

        services.AddDbContext<CustomerContext>(options => options.UseSqlite(dbConnection));
        services.AddDbContext<CustomerContext>();


    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            

      //from ASP
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

      //From Angular
      app.Use(async (context, next) =>
      {
        await next();
        if (context.Response.StatusCode == 400 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "index.html";
          await next();
        }
      });

      app.UseDefaultFiles();
      app.UseMvcWithDefaultRoute();
      app.UseStaticFiles();

      Program.DoDB();
    }
    }
}
