using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using University.User.Application.IServices;
using University.User.ApplicationService.Services;
using University.User.Infrastructure;
using University.User.Infrastructure.Repository;
using University.User.Model.IRepository;
using University.User.Protos;
using StackExchange.Redis;

namespace University.User;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddGrpc();
        var connectionString = _configuration.GetConnectionString("UniversityDatabase");

        //var redisConnetionString = _configuration.GetConnectionString("Redis");
        //if (string.IsNullOrEmpty(redisConnetionString))
        //{
        //    throw new InvalidOperationException("Redis  Connetion String is not configed");
        //}
        //Console.WriteLine($"Redis Connetion String : {_configuration.GetConnectionString("Redis")}");
        services.AddScoped<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse(_configuration.GetConnectionString("Redis"), true);
            
            configuration.AbortOnConnectFail = false; 
            return ConnectionMultiplexer.Connect(configuration);
        });
        services.AddDbContext<UniversityContext>(options =>
           options.UseSqlServer(_configuration.GetConnectionString("UniversityDatabase")));
        services.AddCors(op => op.AddPolicy(name: "MyPolicy",
            policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
            }));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("a3BzY29tYXBpbG90cmltZWRpc2NvdW50Y29tcGxleHRlc3RpbmcyNjRiaXQ=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRedisService, RedisService>();
    }

    public void Configure(IApplicationBuilder builder)
    {
        builder.UseCors("MyPolicy");
        builder.UseSwagger();
        builder.UseSwaggerUI();
       
        builder.UseRouting();
        builder.UseAuthentication(); 
        builder.UseAuthorization(); 

        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcService<UsersService>();
        });
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<TokenServices>();  
            endpoints.MapControllers();  
        });
    }
}




