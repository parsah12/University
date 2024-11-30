using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using University.Course.Application.Dto;
using University.Course.Application.Dto.Enum;
using University.Course.Application.IServices;


namespace University.Course.ApplicationServices.Services;

public class TokenService : ITokenService
{
    public Task<string> GenerateToken(UserDto user)
    {
        if (user == null || string.IsNullOrEmpty(user.Password))
        {
            throw new ArgumentNullException("User or required properties are null.");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName !),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
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
    public async Task<TokenValidateDto> ValidateToken(string token)
    {
        try
        {
            var splitedToken = token.StartsWith("Bearer ") ? token.Split("Bearer ").Last() : token;

            var key = new SymmetricSecurityKey(Convert.FromBase64String("a3BzY29tYXBpbG90cmltZWRpc2NvdW50Y29tcGxleHRlc3RpbmcyNjRiaXQ="));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.FromMinutes(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(splitedToken, validationParameters, out var validatedToken);

            var claims = claimsPrincipal.Claims.Where(e => !string.IsNullOrEmpty(e.Value)).ToList();

            if (claims.Count != 0)
            {
                var userName = claims.SingleOrDefault(e => e.Type == ClaimTypes.Name)?.Value;
                var password = claims.SingleOrDefault(e => e.Type == ClaimTypes.Name)?.Value;
                var firstName = claims.SingleOrDefault(e => e.Type == ClaimTypes.Name)?.Value;
                var lastName = claims.SingleOrDefault(e => e.Type == ClaimTypes.Name)?.Value;
                var role = claims.SingleOrDefault(e => e.Type == ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(role))
                {
                    return new TokenValidateDto
                    {
                        IsAuthenticated = false,
                        Message = "Token is not valid! Please try again with a correct token."
                    };
                }

                return new TokenValidateDto
                {
                    IsAuthenticated = true,
                    Username = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Role = ToRole(role),
                    Message = "Token validated successfully!"
                };
            }
        }
        catch (Exception)
        {
            return new TokenValidateDto
            {
                IsAuthenticated = false,
                Message = "Token is not valid! Please try again with a correct token."
            };
        }

        return new TokenValidateDto
        {
            IsAuthenticated = false,
            Message = "Token is not valid! Please try again with a correct token."
        };
    }

    private RoleEnumDto ToRole(string value)
    {
        return value switch
        {
            "1" => RoleEnumDto.Student,
            "2" => RoleEnumDto.Teacher,
            "3" => RoleEnumDto.Admin,
            _ => throw new Exception("Invalid role value")
        };
    }


}




