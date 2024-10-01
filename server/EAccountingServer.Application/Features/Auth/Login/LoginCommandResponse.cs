using EAccountingServer.Application.Models.Dtos.Users;

namespace EAccountingServer.Application.Features.Auth.Login
{
    public sealed record LoginCommandResponse(
        string Token,
        string RefreshToken,
        DateTime RefreshTokenExpires);
}
