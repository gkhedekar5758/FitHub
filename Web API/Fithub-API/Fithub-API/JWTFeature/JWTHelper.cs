using Fithub_Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_API.JWTFeature
{
  public class JWTHelper
  {
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSection;
    public JWTHelper(IConfiguration configuration)
    {
      _configuration = configuration;
      _jwtSection = configuration.GetSection("JWTSettings");
    }

    /// <summary>
    /// Generate JWT token for anonymus user
    /// </summary>
    /// <param name="user"> User who want to login</param>
    /// <returns></returns>
    public async Task<string> GenerateToken(UserModel user)
    {
      var signInCredetials = GetSignInCredentials();
      var roles = await GetRoles(user);
      var tokenOptions = GetTokenOption(signInCredetials);
      var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


      return token;
    }

    private async Task<List<Claim>> GetRoles(UserModel user)
    {
      //TODO: implemnent to bring the roles from the User
      return null;
    }

    private JwtSecurityToken GetTokenOption(SigningCredentials signInCredetials)
    {
      var option = new JwtSecurityToken(issuer: _jwtSection.GetSection("validIssuer").Value,
        audience: _jwtSection.GetSection("validAudience").Value,
        claims: null, //claim- NULL at this point //TODO- add proper claim here
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSection.GetSection("lifeTimeOfToken").Value)),
        signingCredentials: signInCredetials);

      return option;
    }

    private SigningCredentials GetSignInCredentials()
    {
      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSection.GetSection("securityKey").ToString()));
      return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
    }
  }
}
