using System;
using ECoinTackerFrontEnd.Configuration;
using ECoinTackerFrontEnd.Managers;
using ECoinTackerFrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECoinTackerFrontEnd
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
	        services.Configure<ApplicationConfiguration>(Configuration.GetSection(nameof(ApplicationConfiguration)));

            services.AddMvc();
	        services.AddSession(options =>
	        {
		        options.IdleTimeout = TimeSpan.FromMinutes(30);
		        options.Cookie.Name = "ECoinTracker";
				options.Cookie.HttpOnly = true;
	        });

	        services.AddTransient<ICurrencyManager, CurrencyManager>();
	        services.AddTransient<IAccountManager, AccountManager>();
	        services.AddTransient<IChartManager, ChartManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

	        app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
	            routes.MapRoute(
		            name: "default",
		            template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
