using System;
using System.Threading.Tasks;
using Micro.Services.Identity.Domain.Models;

namespace Micro.Services.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<User> GetAsync(Guid id);

        Task<User> GetAsync(string email);
    }
}
