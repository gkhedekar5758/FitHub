using Fithub_Data.DTO;
using Fithub_Data.Models;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_API.JWTFeature
{
    public class JWTHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSection;
        private readonly IConfigurationSection _googleAuthSetting;
        public JWTHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSection = configuration.GetSection("JWTSettings");
            _googleAuthSetting = configuration.GetSection("GoogleAuthSetting");
        }

        /// <summary>
        /// Generate JWT token for anonymus user
        /// </summary>
        /// <param name="user"> User who want to login</param>
        /// <returns></returns>
        public async Task<string> GenerateToken(User user)
        {
            var signInCredetials = GetSignInCredentials();
            var claims = GetRoles(user);
            var tokenOptions = GetTokenOption(signInCredetials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


            return token;
        }

        private List<Claim> GetRoles(User user)
        {

            var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name,user.Email),
        new Claim(ClaimTypes.Role,user.Role.NormalisedName)
      };
            return claims;
        }

        private JwtSecurityToken GetTokenOption(SigningCredentials signInCredetials, List<Claim> claims)
        {
            var option = new JwtSecurityToken(issuer: _jwtSection.GetSection("validIssuer").Value,
              audience: _jwtSection.GetSection("validAudience").Value,
              claims: claims,
              expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSection.GetSection("lifeTimeOfToken").Value)),
              signingCredentials: signInCredetials);

            return option;
        }

        private SigningCredentials GetSignInCredentials()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSection.GetSection("securityKey").Value));
            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }

        public  GoogleJsonWebSignature.Payload VerifyGoogleToken(ExternalAuthDTO externalAuthDTO)
        {
            try
            {
                var setting = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>()
          {
            _googleAuthSetting.GetSection("clientID").Value
          }
                };

                var payload =  GoogleJsonWebSignature.ValidateAsync(externalAuthDTO.idToken, setting).Result;
                return payload;

            }
            catch (Exception e)
            {
                return null;

            }


        }
    }
}
