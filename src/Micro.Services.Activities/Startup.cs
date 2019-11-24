using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Micro.Common.RabbitMq;
using Micro.Common.Commands;
using Micro.Services.Activities.Handlers;
using Micro.Common.Mongo;
using Micro.Services.Activities.Repositories;
using Micro.Services.Activities.Domain.Repositories;
using Micro.Services.Activities.Services;

namespace Micro.Services.Activities
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
            services.AddRabbitMq(Configuration);
            services.AddMongo(Configuration);
            services.AddTransient<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IDatabaseSeeder, CustomMongoSeeder>();
            services.AddTransient<IActivityService, ActivityService>();
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
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
        }
    }
}
