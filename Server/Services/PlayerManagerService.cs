using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PlayerManager;
using System.Threading.Tasks;

namespace Server.Services
{
    [Authorize]
    public class PlayerManagerService : PlayerManager.PlayerManager.PlayerManagerBase
    {
        private readonly ILogger<PlayerManagerService> _logger;
        private readonly PlayerService _playerService;

        public PlayerManagerService(ILogger<PlayerManagerService> logger, PlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        public override Task<CreateReply> CreatePlayer(CreateRequest request, ServerCallContext context)
        {
            string status;
            if (_playerService.IsNickAvailable(request.Nick))
            {
                _playerService.CreatePlayer(request.Nick);
                status = "ok";
            }
            else
            {
                status = "exist";
            }

            return Task.FromResult(new CreateReply
            {
                Status = status ?? "error"
            });
        }
    }
}