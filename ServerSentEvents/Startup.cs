using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServerSentEvents.Infrastructure;
using ServerSentEvents.Middlewares;

namespace ServerSentEvents
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISseService, SseService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles()
               .MapServerSentEvents("/events")
               .UseMvc(routes=>
                {
                    routes.MapRoute("default", "{controller=Events}/{action=Index}");
                });
        }
    }
}
