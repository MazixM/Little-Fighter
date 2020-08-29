using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlayerControllerProto;
using Server.Services.Model;
using System.Threading.Tasks;

namespace Server.Services
{
    [Authorize]
    public class PlayerController : PlayerControllerProto.PlayerController.PlayerControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly PlayerService _playerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _authUsername;

        public PlayerController(ILogger<PlayerController> logger, PlayerService playerService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _playerService = playerService;
            _httpContextAccessor = httpContextAccessor;
            _authUsername = _httpContextAccessor.HttpContext.User.FindFirst("preferred_username").Value;
        }
        public override Task<CreateReply> CreatePlayer(CreateRequest request, ServerCallContext context)
        {
            string status = null;

            if (_playerService.CreatePlayer(request.Nick, _authUsername).Id != null)
            {
                status = "ok";
            }
            else
            {
                status = "nick_exist";
            }

            return Task.FromResult(new CreateReply
            {
                Status = status ?? "error"
            });
        }
        public override Task<CheckReply> CheckPlayer(CheckRequest request, ServerCallContext context)
        {
            bool isUserHavePlayer = false;
            Player player = _playerService.GetPlayer(_authUsername);

            if (player.Id != null)
            {
                isUserHavePlayer = true;
            }

            return Task.FromResult(new CheckReply
            {
                IsUserHavePlayer = isUserHavePlayer,
                Nick = player.Nick ?? ""
            });
        }
        public override Task<DeleteReply> DeletePlayer(DeleteRequest request, ServerCallContext context)
        {
            bool succes = false;
            Player player = _playerService.GetPlayer(_authUsername);

            if (player != null)
            {
                succes = _playerService.DeletePlayer(_authUsername);
            }

            return Task.FromResult(new DeleteReply
            {
                IsDeleted = succes
            });
        }
    }
}