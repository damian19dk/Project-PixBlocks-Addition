using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PixBlocks_Addition.Api.Framework;
using PixBlocks_Addition.Domain.Contexts;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Domain.Settings;
using PixBlocks_Addition.Infrastructure.Repositories;
using PixBlocks_Addition.Infrastructure.Services;
using PixBlocks_Addition.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Domain;

namespace PixBlocks_Addition.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var sqlSection = Configuration.GetSection("sql");
            services.Configure<SqlSettings>(sqlSection);
            var sqlSettings = new SqlSettings();
            sqlSection.Bind(sqlSettings);

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<PixBlocksContext>()
                .AddDbContext<RefreshTokenContext>();

            var jwtSection = Configuration.GetSection("jwt");
            services.Configure<JwtOptions>(jwtSection);
            var jwtOptions = new JwtOptions();
            jwtSection.Bind(jwtOptions);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(c =>
                {
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = jwtOptions.ValidateLifetime
                    };
                });

            services.AddAuthorization(p => p.AddPolicy("admin", x => x.RequireRole("Admin")));
            
            services.AddOptions();
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<CancellationTokenMiddleware>();
            services.AddTransient<ICancellationTokenService, CancellationTokenService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware<CancellationTokenMiddleware>();
            app.UseMvc();
        }
    }
}
