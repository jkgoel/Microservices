using System.Threading.Tasks;

namespace JKTech.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}