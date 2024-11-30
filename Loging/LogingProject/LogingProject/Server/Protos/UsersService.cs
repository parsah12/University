using Grpc.Core;
using University.Units.Protos;
namespace University.Units.Protos;

public class UsersService : UsersGrpc.UsersGrpcBase
{
    public override async Task<UserResponse> Users(UserRequests request, ServerCallContext context)
    {
        var response = new UserResponse
        {
            IsSent = true,
            Message = "Course details processed successfully",
        };

        response.DataList.Add("Lecture 1");
        response.DataList.Add("Lecture 2");
        response.DataList.Add("Lecture 3");

        return await Task.FromResult(response);
    }
}
