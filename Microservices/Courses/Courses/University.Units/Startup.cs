using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using University.Course.Application.IServices;
using University.Course.ApplicationServices.Services;
using University.Course.Infrastructure.Repository;
using University.Course.Infrastructure;
using University.Course.Model.IRepository;
using University.Course;
using University.User.Grpc;
using StackExchange.Redis;

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

        services.AddSingleton<IConnectionMultiplexer>(sp =>
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
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("a3BzY29tYXBpbG90cmltZWRpc2NvdW50Y29tcGxleHRlc3RpbmcyNjRiaXQ=")),
                    ClockSkew = TimeSpan.Zero
                };

            });
        services.AddGrpcClient<TokenServiceAutoraizeGrpc.TokenServiceAutoraizeGrpcClient>(options =>
        {
            options.Address = new Uri("http://localhost:5008"); 
        });

        services.AddAuthorization();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ITokenService , TokenService>();
        services.AddScoped<IRedisService , RedisService>();
        services.AddGrpc();
    }

    public void Configure(IApplicationBuilder builder)
    {

        builder.UseCors("MyPolicy");
        
        builder.UseSwagger();
        builder.UseSwaggerUI();
        builder.UseMiddleware<TokenValidationMiddleware>();
        builder.UseRouting();
        builder.UseAuthentication();
        builder.UseAuthorization();
        builder.UseEndpoints(setting => setting.MapControllers());
     
    }
}

