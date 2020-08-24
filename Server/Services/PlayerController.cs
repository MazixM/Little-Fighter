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

        public PlayerController(ILogger<PlayerController> logger, PlayerService playerService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _playerService = playerService;
            _httpContextAccessor = httpContextAccessor;
        }
        public override Task<CreateReply> CreatePlayer(CreateRequest request, ServerCallContext context)
        {
            //Getting username from Keycloak username
            string username = _httpContextAccessor.HttpContext.User.FindFirst("preferred_username").Value;
            string status = null;

            if (_playerService.CreatePlayer(request.Nick, username) != null)
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
            string username = _httpContextAccessor.HttpContext.User.FindFirst("preferred_username").Value;
            Player player = _playerService.GetPlayer(username);

            if (player != null)
            {
                isUserHavePlayer = true;
            }
            else
            {
                player = new Player();
            }

            return Task.FromResult(new CheckReply
            {
                IsUserHavePlayer = isUserHavePlayer,
                Nick = player.Nick ?? ""
            });
        }
    }
}