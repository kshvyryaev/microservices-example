using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Micro.Common.Events;
using Micro.Common.RabbitMq;
using Micro.Api.Handlers;
using Micro.Common.Authentication;
using Micro.Api.Repositories;
using Micro.Common.Mongo;

namespace Micro.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddLogging();
            services.AddJwt(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddMongo(Configuration);
            services.AddTransient<IEventHandler<UserCreated>, UserCreatedHandler>();
            services.AddTransient<IEventHandler<UserAuthenticated>, UserAuthenticatedHandler>();
            services.AddTransient<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();
            services.AddTransient<IActivityRepository, ActivityRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            app.UseMvc();
        }
    }
}
