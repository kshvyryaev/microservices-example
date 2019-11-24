using System.Threading.Tasks;

namespace Micro.Common.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
