using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dotnet_cs_api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Session;

namespace dotnet_cs_api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _web;
        public Startup(IConfiguration configuration, IWebHostEnvironment web)
        {
            Configuration = configuration;
            _web = web;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(o => {
                o.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet_cs_api", Version = "v1" });
            });
            services.AddDbContext<csDbContext>(op => op.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
            services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod().
                AllowAnyHeader()
             );
        });
            services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet_cs_api v1"));
            }
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
               Path.Combine(_web.WebRootPath, "public")),
                RequestPath = "/public",
                EnableDefaultFiles = true
            });
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
