using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Repository;
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Repository;
using VXAS5X_HFT_2023241.Endpoint.Services;

namespace VXAS5X_HFT_2023241.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowWebApp",
                    builder => builder.WithOrigins("http://localhost:3000") // React app runs on localhost:3000
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials()); 
            });

            services.AddTransient<IActorLogic, ActorLogic>();
            services.AddTransient<IStagePlayLogic, StagePlayLogic>();
            services.AddTransient<IDramaaturgLogic, DramaturgLogic>();

            services.AddTransient<IActorRepo, ActorRepo>();
            services.AddTransient<IStagePlayRepo, StagePlayRepo>();
            services.AddTransient<IDramaturgRepo, DramaturgRepo>();

            services.AddTransient<StagePlayDbContext, StagePlayDbContext>();

            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            // Apply CORS policy globally
            app.UseCors("AllowWebApp");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
