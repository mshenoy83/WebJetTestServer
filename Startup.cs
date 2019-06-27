using System;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

using WebjetTestServer.Services;

namespace WebjetTestServer
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IMovieRepository, CinemaworldMovieRepository>();
            services.AddTransient<IMovieRepository, FilmworldMovieRepository>();

            services.AddHttpClient("CinemaWorldClient",c=>
            {
                c.BaseAddress = new System.Uri("https://webjetapitest.azurewebsites.net/api/cinemaworld/");
                c.DefaultRequestHeaders.Add("x-access-token", "sjd1HfkjU83ksdsm3802k");
                c.Timeout = TimeSpan.FromSeconds(15);
            });

            services.AddHttpClient("FilmWorldClient", c =>
            {
                c.BaseAddress = new System.Uri("https://webjetapitest.azurewebsites.net/api/filmworld/");
                c.DefaultRequestHeaders.Add("x-access-token", "sjd1HfkjU83ksdsm3802k");
                c.Timeout = TimeSpan.FromSeconds(15);
            });

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebJetApi.xml"));
                c.DescribeAllEnumsAsStrings();
                c.SwaggerDoc("v1", new Info { Title = "WebJet API", Version = "v1" });
            });
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

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "WebJet API V1"); });
            app.UseMvc();
        }
    }
}
