using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repositories;

namespace Micro.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private const string CollectionName = "Categories";

        private readonly IMongoDatabase _database;

        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<Category> Collection
            => _database.GetCollection<Category>(CollectionName);

        public async Task<Category> GetAsync(string name)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());

        public async Task<IEnumerable<Category>> BrowseAsync()
            => await Collection
                .AsQueryable()
                .ToListAsync();

        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);
    }
}
