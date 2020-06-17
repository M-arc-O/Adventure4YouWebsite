﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using AutoMapper;

using Adventure4YouAPI.Auth;
using Adventure4You.Helpers;
using Adventure4YouAPI.Helpers;

namespace Adventure4YouAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DBContextHelper.AddDBContext(services, Configuration);
            DBContextHelper.AddAuthentication(services);
            JwtHelper.AddJwt(services, Configuration);

            BLHelper.AddBLs(services);

            MapperHelper.AddAutoMapper(services);

            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RaceUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();            

            services.AddAutoMapper(typeof(Startup));
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(a => a.SerializerSettings.Converters.Add(new TrimmingJsonConverter()));
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

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
