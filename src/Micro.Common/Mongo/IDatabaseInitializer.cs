using System.Threading.Tasks;

namespace Micro.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
