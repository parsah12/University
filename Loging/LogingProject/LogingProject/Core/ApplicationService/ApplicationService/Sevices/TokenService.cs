using Microsoft.IdentityModel.Tokens;
using Project.Core.Application.Dto;
using Project.Core.Application.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project.Core.ApplicationService.Sevices;

public class TokenService : ITokenService
{
    public Task<string> GenerateToken(UserDto user)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

        var key = new SymmetricSecurityKey(Convert.FromBase64String("a3BzY29tYXBpbG90cmltZWRpc2NvdW50Y29tcGxleHRlc3RpbmcyNjRiaXQ="));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddHours(6),
            signingCredentials: creds
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

}




