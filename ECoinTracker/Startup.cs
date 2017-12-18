using System;
using System.Threading.Tasks;
using ECoinTracker.Configurations;
using ECoinTracker.Contexts;
using ECoinTracker.Managers;
using ECoinTracker.Managers.Interfaces;
using ECoinTrackerModels.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ECoinTracker
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
	        // Add configurations:
	        services.AddOptions();
	        services.Configure<ApplicationConfiguration>(Configuration.GetSection(nameof(ApplicationConfiguration)));

			// Add authentication:
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		        .AddJwtBearer(options =>
		        {
			        {
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = Configuration.GetSection(nameof(ApplicationConfiguration))["IssuerName"],
							ValidAudience = Configuration.GetSection(nameof(ApplicationConfiguration))["AudienceName"],
							IssuerSigningKey = JwtSecurityKey.Create(Configuration.GetSection(nameof(ApplicationConfiguration))["JwtTokenSecretKey"])
						};

						options.Events = new JwtBearerEvents
				        {
					        OnAuthenticationFailed = context =>
					        {
						        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
						        return Task.CompletedTask;
					        },
					        OnTokenValidated = context =>
					        {
						        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
						        return Task.CompletedTask;
					        }
				        };
					}
		        });

			// Add cors:
	        services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
	        {
		        builder.AllowAnyOrigin()
			        .AllowAnyMethod()
			        .AllowAnyHeader();
	        }));

			// Add database:
			services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			// Add mvc:
			services.AddMvc();

			// Add application managers:
	        services.AddTransient<ICurrencyManager, CurrencyManager>();
	        services.AddTransient<IChartManager, ChartManager>();
	        services.AddTransient<IAccountManager, AccountManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

	        app.UseAuthentication();
	        app.UseCors("MyPolicy");
			app.UseMvc();

        }
	
    }
}
