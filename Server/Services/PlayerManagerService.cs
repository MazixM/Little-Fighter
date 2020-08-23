using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlayerManager;
using Server.Services.Model;
using SolutionShared;
using System.Threading.Tasks;

namespace Server.Services
{
    [Authorize]
    public class PlayerManagerService : PlayerManager.PlayerManager.PlayerManagerBase
    {
        private readonly ILogger<PlayerManagerService> _logger;
        private readonly PlayerService _playerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlayerManagerService(ILogger<PlayerManagerService> logger, PlayerService playerService, IHttpContextAccessor httpContextAccessor)
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

            if (InputValidationCheck.Nick(request.Nick) || _playerService.IsUsernameExist(username))
            {
                if (_playerService.IsNickAvailable(request.Nick))
                {
                    _playerService.CreatePlayer(request.Nick, username);
                    status = "ok";
                }
                else
                {
                    status = "exist";
                }
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