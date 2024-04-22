using GloboTicket.TicketManagement.Application.Contracts;
using System.Security.Claims;

namespace GloboTicket.TicketManagement.Api.Services
{
    public class LoggedInUserService(IHttpContextAccessor httpContextAccessor) : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;

        public string? UserId => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
