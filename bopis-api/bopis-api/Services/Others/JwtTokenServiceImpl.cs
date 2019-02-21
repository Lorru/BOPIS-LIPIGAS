using bopis_api.Models.Bopis;
using bopis_api.Services.Bopis;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace bopis_api.Services.Others
{
    public class JwtTokenServiceImpl : IJwtTokenService
    {
        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private string key = "TOKEN";

        public string generate(string email)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            int jwtTokenExpires = Convert.ToInt32(configurations[0].Value);
            string jwtTokenKey = configurations[1].Value;

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenKey));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Email, email)
                        }),
                Expires = DateTime.UtcNow.AddMinutes(jwtTokenExpires),
                SigningCredentials = signingCredentials
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public bool validate(string token)
        {
            try
            {
                List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

                string jwtTokenKey = configurations[1].Value;

                SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenKey));
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = symmetricSecurityKey
                };

                SecurityToken securityToken;
                jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
