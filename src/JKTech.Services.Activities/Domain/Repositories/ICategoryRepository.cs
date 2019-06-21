using System.Collections.Generic;
using System.Threading.Tasks;
using JKTech.Services.Activities.Domain.Models;

namespace JKTech.Services.Activities.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        Task<IEnumerable<Category>> BrowseAsync();
        Task AddAsync(Category category);
    }
}