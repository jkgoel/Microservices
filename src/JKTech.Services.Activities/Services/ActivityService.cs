using System;
using System.Threading.Tasks;
using JKTech.Common.Exceptions;
using JKTech.Services.Activities.Domain.Models;
using JKTech.Services.Activities.Domain.Repositories;

namespace JKTech.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCategory = await _categoryRepository.GetAsync(category);
            if (activityCategory == null)
            {
                throw new JKTechException("category_not_found",$"Category: '{category} was not found" );
            }

            var activity = new Activity(id, userId, name, category, description, createdAt);
            await _activityRepository.AddAsync(activity);
        }
    }
}