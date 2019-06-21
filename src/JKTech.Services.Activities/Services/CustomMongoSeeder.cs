using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JKTech.Common.Mongo;
using JKTech.Services.Activities.Domain.Models;
using JKTech.Services.Activities.Domain.Repositories;
using MongoDB.Driver;

namespace JKTech.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;
        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepository) : base(database)
        {
            _categoryRepository = categoryRepository;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>
            {
                "work",
                "sport",
                "hobby"
            };
            await Task.WhenAll(categories.Select(x => _categoryRepository.AddAsync(new Category(x))));
        }
    }
}