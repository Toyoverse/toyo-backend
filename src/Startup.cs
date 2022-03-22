using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BackendToyo.Data;
using Microsoft.EntityFrameworkCore;
using BackendToyo.Services;
using BackendToyo.Models.DataEntities;
using BackendToyo.Data.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.IdentityModel.Tokens;
using BackendToyo.Utils;
using BackendToyo.Services.Implementations;
using BackendToyo.Repositories;
using BackendToyo.Repositories.Implementations;

namespace BackendToyo
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
            string sqlConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("*");
                });
            });
            addServices(services);
            addContextConfigurations(services);
            addRepositories(services);

            //services.AddDbContext<AppDbContext>(options => options.UseMySql(sqlConnection, ServerVersion.AutoDetect(sqlConnection)));
            services.AddDbContext<AppDbContext>(options => options.UseMySql(sqlConnection, ServerVersion.AutoDetect(sqlConnection)));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendToyo", Version = "v1" });
            });
            addAuthentication(services);
        }

        private void addRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }

        private void addAuthentication(IServiceCollection services)
        {
            var jwtKey = Configuration[EnvironmentVariablesUtils.JWT_SECRET_KEY].ToByteArray();
            services.AddAuthentication(
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(
                x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );
        }

        // This method add implementations of 
        private void addServices(IServiceCollection services)
        {
            services.AddScoped<ISortRaffleService, SortRaffleService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ITokenService, TokenService>();
        }

        private void addContextConfigurations(IServiceCollection services)
        {
            services.AddSingleton<IEntityTypeConfiguration<TypeToken>, TypeTokenConfiguration>();
            services.AddSingleton<IEntityTypeConfiguration<BoxType>, BoxTypeConfiguration>();
            services.AddSingleton<IEntityTypeConfiguration<UserInfo>, UserInfoConfiguration>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendToyo v1"));
            }

            //app.UseHttpsRedirection();
            app.ConfigureExceptionHandler();
            app.UseRouting();

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
