using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Application.IServices;
using Project.Core.ApplicationService.Sevices;
using Project.Core.Model.IRepository;
using Project.Infrastructure.Repository;
using Project.Infrastructure.Repository.Repository;
using University.Units.Protos;

namespace server;

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

        var connectionString = _configuration.GetConnectionString("UniversityDatabase");

        
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
            });

        services.AddAuthorization();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();



        services.AddGrpc();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ITeacherService, TeacherService>();
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
    }
}
