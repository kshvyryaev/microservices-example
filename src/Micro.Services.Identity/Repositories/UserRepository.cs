using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Micro.Services.Identity.Domain.Models;
using Micro.Services.Identity.Domain.Repositories;

namespace Micro.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string CollectionName = "Users";

        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<User> Collection
            => _database.GetCollection<User>(CollectionName);

        public async Task AddAsync(User user)
            => await Collection.InsertOneAsync(user);

        public async Task<User> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant());
    }
}
