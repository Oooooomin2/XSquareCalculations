using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Persistance;
using XSquareCalculationsApi.Repositories;
using XSquareCalculationsApi.Services.Templates;

namespace XSquareCalculationsApi
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
            services.AddControllers();
            services.AddDbContext<XSquareCalculationContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("XSquareCalculationContext"), new MySqlServerVersion(new Version(5, 7, 36))));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "XSquareCalculationsApi", Version = "v1" });
            });


            services.AddSingleton<ISystemDate, SystemDate>();
            services.AddScoped<IResolveAthenticateRepository, ResolveAuthenticateRepository>();
            services.AddScoped<IResolveTemplatesRepository, ResolveTemplatesRepository>();
            services.AddScoped<IResolveUsersRepository, ResolveUsersRepository>();
            services.AddScoped<IDownloadTemplateService, DownloadTemplateService>();
            services.AddSingleton<IPassword, Password>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "XSquareCalculationsApi v1"));
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
