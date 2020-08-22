using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PlayerManager;
using Server.Services.Model;
using System.Threading.Tasks;

namespace Server.Services
{
    [Authorize]
    public class PlayerManagerService:PlayerManager.PlayerManager.PlayerManagerBase
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
            Player player = _playerService.CreatePlayer(request.Nick);
            return Task.FromResult(new CreateReply
            {
                Status = "ok"
            });
        }
    }
}