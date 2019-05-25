﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using PixBlocks_Addition.Domain.Repositories.MediaRepo;
using PixBlocks_Addition.Infrastructure.Services.MediaServices;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddCors();

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

            var jwPlayerSection = Configuration.GetSection("jwPlayer");
            services.Configure<JWPlayerOptions>(jwPlayerSection);
            var jwPlayerOptions = new JWPlayerOptions();
            jwPlayerSection.Bind(jwPlayerOptions);

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

            services.AddAuthorization(p => p.AddPolicy("Premium", x => x.RequireClaim("Premium","True")));

            services.AddOptions();
            services.AddSingleton<IJwtHandler, JwtHandler>(sp =>
            {
                var handlerOptions = sp.GetService<IOptions<JwtOptions>>();
                return new JwtHandler(handlerOptions);
            });
            services.AddSingleton<IJwtPlayerHandler, JwtHandler>(sp =>
            {
                var handlerOptions = sp.GetService<IOptions<JWPlayerOptions>>();
                return new JwtHandler(handlerOptions);
            });
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSingleton<IJWPlayerAuthHandler, JWPlayerAuthHandler>();

            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();

            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IExerciseService, ExerciseService>();

            services.AddHttpClient<IJWPlayerService, JWPlayerService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<CancellationTokenMiddleware>();
            services.AddTransient<ICancellationTokenService, CancellationTokenService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "PixBlocks Addition", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            app.UseCors(builder =>
                builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials()
                );
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PixBlocks Addition V1");
            });

            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware<CancellationTokenMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMvc();
        }
    }
}
