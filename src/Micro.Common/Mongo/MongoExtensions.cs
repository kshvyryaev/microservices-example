using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Micro.Common.Mongo
{
    public static class MongoExtensions
    {
        private const string MongoSectionKey = "Mongo";

        public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection(MongoSectionKey));

            services.AddSingleton(x =>
            {
                var options = x.GetService<IOptions<MongoOptions>>();

                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddTransient(x =>
            {
                var options = x.GetService<IOptions<MongoOptions>>();
                var client = x.GetService<MongoClient>();

                return client.GetDatabase(options.Value.DatabaseName);
            });

            services.AddTransient<IDatabaseInitializer, MongoInitializer>();
            services.AddTransient<IDatabaseSeeder, MongoSeeder>();
        }
    }
}
