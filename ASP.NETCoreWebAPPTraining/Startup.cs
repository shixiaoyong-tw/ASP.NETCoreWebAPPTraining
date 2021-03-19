using ASP.NETCoreWebAPPTraining.Filters;
using ASP.NETCoreWebAPPTraining.MiddleWares;
using ASP.NETCoreWebAPPTraining.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASP.NETCoreWebAPPTraining
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
            services.AddControllers(option =>
            {
                option.Filters.Add<MyActionFilter>();
                // option.Filters.Add<MyAuthorizationFilter>();
            });
            // services.AddAuthentication(ac=>);
            services.AddScoped<IStudentService, StudentService>();
            services.AddHttpContextAccessor();
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(next => new RequestDelegate(async context =>
            {
                context.Request.EnableBuffering();
                await next(context);
            }));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();


            app.UseMiddleware<MyMiddleWare>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}