﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micro.Api.Models;

namespace Micro.Api.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);

        Task<IEnumerable<Activity>> BrowseAsync(Guid userId);

        Task AddAsync(Activity activity);
    }
}
