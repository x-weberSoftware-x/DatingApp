using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    //implement interface function
    public string CreateToken(AppUser user)
    {
        //get the token key setting from our config file
        //?? means if this is null then run whats o nteh right side of ??
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenkey from appsettings");
        //check token key length, min security says key needs to be a min length
        if (tokenKey.Length < 64) throw new Exception("Your tokenKey needs to be longer");
        //set key to be JWT token using system.identitymodel.tokens.jwt nuget package
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        //make a new claim for the username
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName)
        };

        //when using this security alogorithm we need a token key with a length > 64. 
        //that is why we did the length check above
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            //after a week our token will no longer be valid
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        //create the token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        //returnt the token
        return tokenHandler.WriteToken(token);
    }
}
