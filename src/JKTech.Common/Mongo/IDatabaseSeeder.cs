using System.Threading.Tasks;

namespace JKTech.Common.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}