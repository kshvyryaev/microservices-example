using System;
using System.Threading.Tasks;
using Micro.Services.Activities.Domain.Models;

namespace Micro.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);

        Task AddAsync(Activity activity);
    }
}
