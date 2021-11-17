using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSquareCalculationsApi.Configs;

namespace XSquareCalculationsApi.Models
{
    public interface IResolveJwtAuthenticate
    {
        JwtResponse CreateJwtResponse(int userId, string userName);
    }

    public class ResolveJwtAuthenticate : IResolveJwtAuthenticate
    {
        private readonly JwtSettingsConfig _jwtSettingsConfig;
        private readonly ISystemDate _systemDate;

        public ResolveJwtAuthenticate(IOptions<JwtSettingsConfig> jwtSettingsConfig, ISystemDate systemDate)
        {
            _jwtSettingsConfig = jwtSettingsConfig.Value;
            _systemDate = systemDate;
        }

        public JwtResponse CreateJwtResponse(int userId, string userName)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettingsConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(JwtRegisteredClaimNames.GivenName, userName) }),
                Audience = _jwtSettingsConfig.SiteUrl,
                Issuer = _jwtSettingsConfig.SiteUrl,
                SigningCredentials = new SigningCredentials
                (
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenText = tokenHandler.WriteToken(token);
            var now = _systemDate.GetSystemDate();
            return new JwtResponse
            {
                UserId = userId,
                IdToken = tokenText,
                ExpiredDateTime = now.AddDays(_jwtSettingsConfig.ExpiredDay),
                CreatedTime = now
            };
        }
    }
}
