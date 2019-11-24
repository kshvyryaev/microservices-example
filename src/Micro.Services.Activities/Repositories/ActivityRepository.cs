using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repositories;

namespace Micro.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private const string CollectionName = "Activities";

        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>(CollectionName);

        public async Task<Activity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);
    }
}
