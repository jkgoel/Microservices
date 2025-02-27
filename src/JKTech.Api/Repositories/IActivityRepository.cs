﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JKTech.Api.Models;

namespace JKTech.Api.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task<IEnumerable<Activity>> BrowseAsync(Guid userId);
        Task AddAsync(Activity activity);
    }
}
