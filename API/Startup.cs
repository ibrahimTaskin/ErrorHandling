using API.Data;
using API.Data.Interfaces;
using API.Middlewares.CustomExceptionMiddleware;
using API.Models;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Settings;
using API.Validation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace API
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

            services.AddControllers().AddFluentValidation(config=> {
                config.RegisterValidatorsFromAssemblyContaining<ProductValidator>();
            });

            // Secret Json içindeki TestDatabaseSettings içindeki verileri al.
            services.Configure<ProductDatabaseSettings>(Configuration.GetSection("TestDatabaseSettings"));

            // Yukarýdaki Configure içinden aldýðýmýz veriyi , IProductDatabaseSettings'i nerede kullanýrsan DI ile
            // bize Database özelliklerini getirecek
            services.AddSingleton<IProductDatabaseSettings>(opt =>
                opt.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);

            services.AddTransient<IProductContext, ProductContext>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Product> logger)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

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
