using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Services;
using ComputerComplectorWebAPI.Models;

using Microsoft.EntityFrameworkCore;
using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Helpers;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ComputerComplectorWebService
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
			#region Authorization

			services.AddCors();

			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// configure jwt authentication
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			services.AddScoped<IUserService, UserService>();

			#endregion

			appSettings.Secret = null;
			services.AddSingleton(appSettings);

			string componentsConnection = Configuration.GetConnectionString("DefaultConnection");
			//string componentsConnection = Configuration.GetConnectionString("TestConnection");
			string usersConnection = Configuration.GetConnectionString("UsersConnection");
			string analyticsConnection = Configuration.GetConnectionString("AnalyticsConnection");

			//if (Environment.GetEnvironmentVariable("USE_ENTITY") == "true")
			//{
			services.AddDbContext<ComponentsContext>(options => options.UseSqlServer(componentsConnection));
			services.AddScoped<IComponentsServiceAsync, ComponentsContextServiceAsync>();
			//}
			//else
			//{
			//	services.AddScoped<IUtilityAsync>((conn) => new DBUtility(componentsConnection));
			//	services.AddScoped<IComponentsServiceAsync, ComponentsServiceAsync>();
			//}

			services.AddDbContext<UsersContext>(options => options.UseSqlServer(usersConnection));

			services.AddDbContext<StatisticsContext>(options => options.UseSqlServer(analyticsConnection));
			services.AddScoped<IAnalyticsServiceAsync, AnalyticsServiceAsync>();

			services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseAuthentication();

			app.UseMvc();
		}
    }
}
