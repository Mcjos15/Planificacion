using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Backend.DataBase;
using Backend.Interfaces;
using Backend.Services;
using Microsoft.Extensions.Options;

namespace Backend
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
            
            services.AddMvc();
            services.Configure<Mongo>(options =>
            {
                options.ConnectionString
                    = Configuration.GetSection("proyectoDB:ConnectionString").Value;
                options.DatabaseName
                    = Configuration.GetSection("proyectoDB:DatabaseName").Value;

            });

            //Singleton objects are the same for every object and every request.
            //services.AddSingleton<UserService>();
            /*Transient objects are always different; a new instance is provided to every controller and every service
            services.AddTransient<IUser, UserService>();*/
            services.AddTransient<IUser, UserService>();
            services.AddSingleton<IConfiguraciones,ConfiguracionesService>();
            services.AddSingleton<IDocument, DocumentService>();
            services.AddSingleton<IBloque, BloqueService>();
            services.AddSingleton<IMining, MiningService>();
            services.AddControllers().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
         builder => builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
