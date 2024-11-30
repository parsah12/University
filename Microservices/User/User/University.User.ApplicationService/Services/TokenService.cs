using System.Security.Claims;
using University.User.Application.Dto;
using University.User.Application.IServices;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using University.User.Application.Dto.Enum;
using System.Text;

namespace University.User.ApplicationService.Services;

public class TokenService : ITokenService
{
    private const string SecretKey = "a3BzY29tYXBpbG90cmltZWRpc2NvdW50Y29tcGxleHRlc3RpbmcyNjRiaXQ=";
    public Task<string> GenerateToken(UserDto user)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName !),
        new Claim(ClaimTypes.Role, user.Role.ToString()),
    };

        var key = new SymmetricSecurityKey(Convert.FromBase64String(SecretKey));
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

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(SecretKey)),
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(splitedToken, validationParameters, out var validatedToken);

            if (!(validatedToken is JwtSecurityToken jwtToken) || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return new TokenValidateDto
                {
                    IsAuthenticated = false,
                    Message = "Invalid token signature"
                };
            }

            var claims = claimsPrincipal.Claims.ToList();
            var userName = claims.SingleOrDefault(e => e.Type == ClaimTypes.Name)?.Value;
            var userId = claims.SingleOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
            var firstName = claims.SingleOrDefault(e => e.Type == "FirstName")?.Value;
            var lastName = claims.SingleOrDefault(e => e.Type == "LastName")?.Value;
            var role = claims.SingleOrDefault(e => e.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(role))
            {
                return new TokenValidateDto
                {
                    IsAuthenticated = false,
                    Message = "Token data is invalid"
                };
            }

            return new TokenValidateDto
            {
                IsAuthenticated = true,
                Username = userName,
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Role = ToRole(role) 
            };
        }
        catch (SecurityTokenExpiredException)
        {
            return new TokenValidateDto
            {
                IsAuthenticated = false,
                Message = "Token has expired"
            };
        }
        catch (SecurityTokenException ex)
        {
            return new TokenValidateDto
            {
                IsAuthenticated = false,
                Message = $"Token validation failed: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new TokenValidateDto
            {
                IsAuthenticated = false,
                Message = $"An unexpected error occurred: {ex.Message}"
            };
        }
    }

    private RoleEnumDto ToRole(string value)
    {
        switch (value)
        {
            case "Student":
                {
                    return RoleEnumDto.Student;

                }
            case "Teacher":
                {
                    return RoleEnumDto.Teacher;

                }
            case "Admin":
                {
                    return RoleEnumDto.Admin;

                }
            default:
                break;
        }
        throw new Exception();
    }
}









