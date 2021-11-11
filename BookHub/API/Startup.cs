using API.Hubs;
using Infrastructure.Contexts;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Automapper 
            services.AddAutoMapper(typeof(DtoModule));

            // Enable CORS 
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigins", options => options.AllowAnyOrigin().AllowAnyMethod()
                    .AllowAnyHeader());
            });

            // JSON Serializer
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                    .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver());

            services.AddControllers();

            // Contexts
            services.AddDbContext<BookContext>(x =>
                x.UseSqlServer(_configuration.GetConnectionString("BookLoopConn")));

            // SignalR
            services.AddSignalR();

            // Repositories
            services.AddRepositories();

            // Custom services 
            services.AddServices();

            // Helper classes
            services.AddHelpers();

            // Host info accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<BookHub>("/bookHub");
            }); 

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "BookPosts")),
                RequestPath = "/BookPosts"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "ProfilePhotos")),
                RequestPath = "/ProfilePhotos"
            });
        }
    }
}
