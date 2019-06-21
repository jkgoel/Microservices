using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JKTech.Services.Activities.Domain.Models;

namespace JKTech.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task AddAsync(Activity activity);
    }
}