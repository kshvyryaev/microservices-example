using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Micro.Common.Authentication
{
    public class JwtHandler : IJwtHandler
    {
        private const string Sub = "sub";
        private const string Iss = "iss";
        private const string Iat = "iat";
        private const string Exp = "exp";
        private const string UniqueName = "unique_name";

        private readonly JwtOptions _options;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly SecurityKey _issuerSigninKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly DateTime _centuryBegin;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _issuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigninKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
        }

        public JsonWebToken Create(Guid userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expiresUtc = nowUtc.AddMinutes(_options.ExpiryMinutes);

            var issuerAt = (long)new TimeSpan(nowUtc.Ticks - _centuryBegin.Ticks).TotalSeconds;
            var expiresAt = (long)new TimeSpan(expiresUtc.Ticks - _centuryBegin.Ticks).TotalSeconds;

            var payload = new JwtPayload
            {
                { Sub, userId },
                { Iss, _options.Issuer },
                { Iat, issuerAt },
                { Exp, expiresAt },
                { UniqueName, userId }
            };

            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                Token = token,
                Expires = expiresAt
            };
        }
    }
}
