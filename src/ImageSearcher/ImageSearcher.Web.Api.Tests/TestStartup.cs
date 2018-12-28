﻿using System.Net.Http;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using ImageSearcher.Core.Interfaces;
using ImageSearcher.Infrastructure.MapperProfiles;
using ImageSearcher.Web.Api.Models.MapperProfiles;
using ImageSearcher.Web.Api.Models.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageSearcher.Web.Api.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation();
            services.AddTransient<IValidator<SearchImages>, SearchImagesValidator>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IHttpHandler, TestHttpHandler>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SearchProfile>();
                cfg.AddProfile<GetByIdProfile>();
                cfg.AddProfile<ImageFilterProfile>();
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IImageService, TestImageService>();
            services.AddScoped<ICachingService, TestCachingService>();
        }
    }
}
