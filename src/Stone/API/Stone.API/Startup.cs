using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossCutting.Identity.Authorization;
using CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stone.API.ApplicationServices;
using Stone.API.Helpers;
using Stone.API.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Stone.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            //  services.AddOptions();
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilter));
            });
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Stone API",
                    Description = "API"
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme()
                    {
                        In = "header",
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    }
                );
            });

            services.AddAuthorization();
            services.AddScoped<IClienteApplicationService, ClienteApplicationService>();
            services.AddScoped<IEstabelecimentoApplicationService, EstabelecimentoApplicationService>();
            services.AddScoped<IPagamentoApplicationService, PagamentoApplicationService>();
            services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var jwtOptions = new JwtTokenOptions(Configuration);
            app.UseJwtBearerAuthentication(jwtOptions);

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Stone  API v1.0");
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
