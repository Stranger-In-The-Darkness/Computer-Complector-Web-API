//#define USE_TEST_SERVER

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
using ComputerComplectorWebAPI.Models.Data.Special;

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

			appSettings.Secret = null;

			#endregion

			services.AddSingleton(appSettings);

#if USE_TEST_SERVER
			string componentsConnection = Configuration.GetConnectionString("TestConnection");
#else
			string componentsConnection = Configuration.GetConnectionString("DefaultConnection");
#endif
			string usersConnection = Configuration.GetConnectionString("UsersConnection");

			services.AddDbContext<ComponentsContext>(options => options.UseSqlServer(componentsConnection));
			services.AddDbContext<UsersContext>(options => options.UseSqlServer(usersConnection));

			services.AddScoped<IComponentsServiceAsync, ComponentsContextServiceAsync>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IStatisticsServiceAsync, AnalyticsServiceAsync>();

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
