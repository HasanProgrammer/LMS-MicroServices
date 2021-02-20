using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebFramework.Extensions.APIStatus;
using WebFramework.Extensions.MongoDatabase;
using WebFramework.Extensions.ServiceContainer;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services) /*Service Container*/
        {
            services.AddControllers();
            
            /*-------------------------------------------------------*/
            
            /*Hasan's Codes*/
            services.AddMongoDatabaseToConfigure(Configuration);
            services.AddMongoDatabaseToServiceContainer(Configuration);
            services.AddPureServicesToServiceContainer();
            services.AddAPIStatusToConfigure(Configuration);
            /*Hasan's Codes*/
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) /*Middleware*/
        {
            app.UseExceptionHandler();
            
            /*-------------------------------------------------------*/
            
            if (env.IsDevelopment())
            {
                
            }
            else
            {
                app.UseHsts();
            }

            /*-------------------------------------------------------*/
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            /*-------------------------------------------------------*/

            app.UseAuthentication();
            
            /*-------------------------------------------------------*/

            app.UseRouting();
            
            /*-------------------------------------------------------*/

            app.UseCors("CORS");
            
            /*-------------------------------------------------------*/

            app.UseAuthorization();
            
            /*-------------------------------------------------------*/

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}