using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using musicList2.Database;
using musicList2.Models;

namespace musicList2
{
    public class Startup
    {
        // Config which is getting passed by ID from Main
        public IConfiguration Configuration { get; }
        private AppDbContext db;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            using (db = new AppDbContext(configuration))
            {
                db.Database.Migrate();
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOptions();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<AppDbContext>();

            services
                .AddTransient<IKeywordAccessLayer, KeywordAccessLayer>();

            services
                .AddSingleton<IConfiguration>(Configuration);

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt => {
                    opt.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = async context =>
                        {
                            context.Response.StatusCode = 401;
                            var errorData = Encoding.UTF8.GetBytes("{\"code\": 401,\"message\": \"unauthorized\"}");
                            await context.Response.Body.WriteAsync(errorData);
                        }
                    };

                    opt.LogoutPath = opt.LoginPath;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Server", "Music List");
                await next();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
