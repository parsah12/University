using Grpc.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using University.User.Grpc;

namespace University.Course;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TokenServiceAutoraizeGrpc.TokenServiceAutoraizeGrpcClient _tokenServiceClient;

    public TokenValidationMiddleware(RequestDelegate next, TokenServiceAutoraizeGrpc.TokenServiceAutoraizeGrpcClient tokenServiceClient)
    {
        _next = next;
        _tokenServiceClient = tokenServiceClient;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authorization header is missing");
            return;
        }

        var token = authorizationHeader.Split("Bearer ").LastOrDefault();

        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Bearer token is missing");
            return;
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);


            var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid token data");
                return;
            }

            var loginResponse = await _tokenServiceClient.LoginAsync(new LoginRequest { Token = token});

            if (!loginResponse.IsAthenticated)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid or expired token");
                return;
            }

            await _next(context);
        }
        catch (SecurityTokenException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid token format");
        }
        catch (RpcException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid token");
        }
    }
}
