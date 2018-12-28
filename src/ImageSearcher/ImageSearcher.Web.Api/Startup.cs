using AutoMapper;
using ImageSearcher.Core.Interfaces;
using ImageSearcher.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using FluentValidation;
using FluentValidation.AspNetCore;
using ImageSearcher.Infrastructure.MapperProfiles;
using ImageSearcher.Web.Api.Models.MapperProfiles;
using ImageSearcher.Web.Api.Models.Request;
using Swashbuckle.AspNetCore.Swagger;

namespace ImageSearcher.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation();
            services.AddTransient<IValidator<SearchImages>, SearchImagesValidator>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IHttpHandler, HttpHandler>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SearchProfile>();
                cfg.AddProfile<GetByIdProfile>();
                cfg.AddProfile<ImageFilterProfile>();
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IImageService, ImageService>(x => new ImageService(x.GetService<IHttpHandler>(), Configuration["FlickrApiKey"], mapper));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
