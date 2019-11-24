using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Micro.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private const string ConventionsRegistryName = "MicroConventions";

        private readonly IMongoDatabase _database;
        private readonly IDatabaseSeeder _seeder;
        private readonly bool _seed;
        private bool _initialized;

        public MongoInitializer(
            IMongoDatabase database,
            IDatabaseSeeder seeder,
            IOptions<MongoOptions> options)
        {
            _database = database;
            _seeder = seeder;
            _seed = options.Value.Seed;
        }

        public async Task InitializeAsync()
        {
            if (!_initialized)
            {
                RegisterConventions();
                _initialized = true;
            }

            if (_seed)
            {
                await _seeder.SeedAsync();
            }
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register(ConventionsRegistryName, new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
