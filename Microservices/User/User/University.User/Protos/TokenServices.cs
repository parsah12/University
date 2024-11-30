using Grpc.Core;
using University.User.Application.IServices;
using University.User.Application.Requests;
using University.User.Grpc;

namespace University.User.Protos
{
    public class TokenServices : TokenServiceAutoraizeGrpc.TokenServiceAutoraizeGrpcBase
    {
        private readonly ITokenService _tokenService;

        public TokenServices(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {

            var userDto = new AuthenticationDto()
            {
                Token = request.Token
            };

            var loginResult = await _tokenService.ValidateToken(userDto.Token);

            return new LoginResponse
            {
                IsAthenticated = loginResult.IsAuthenticated ?? false
            };
        }
    }
}
