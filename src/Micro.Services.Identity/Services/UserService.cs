using System.Threading.Tasks;
using Micro.Common.Authentication;
using Micro.Common.Exceptions;
using Micro.Services.Identity.Domain.Models;
using Micro.Services.Identity.Domain.Repositories;
using Micro.Services.Identity.Domain.Services;

namespace Micro.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(
            IUserRepository userRepository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null)
            {
                throw new ValidationException("email_in_use", $"Email '{email}' is already in use");
            }

            user = new User(email, name);
            user.SetPassword(password, _encrypter);
            await _userRepository.AddAsync(user);
        }

        public async Task<JsonWebToken> LogginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
            {
                throw new ValidationException("invalid_credentials", $"Invalid credentials");
            }

            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new ValidationException("invalid_credentials", $"Invalid credentials");
            }

            return _jwtHandler.Create(user.Id);
        }
    }
}
