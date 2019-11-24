using System.Threading.Tasks;
using Micro.Common.Authentication;

namespace Micro.Services.Identity.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string name);

        Task<JsonWebToken> LogginAsync(string email, string password);
    }
}
