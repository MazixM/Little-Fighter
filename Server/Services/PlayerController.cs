using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
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
        private readonly HttpContext _httpContext;

        public PlayerController(ILogger<PlayerController> logger, PlayerService playerService, HttpContext httpContext)
        {
            _logger = logger;
            _playerService = playerService;
            _httpContext = httpContext;
        }
        public override Task<CreateReply> CreatePlayer(CreateRequest request, ServerCallContext context)
        {
            string status;

            if (_playerService.CreatePlayer(request.Nick, _httpContext.GetUsername()).Id != null)
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
        public override Task<CheckReply> CheckPlayer(EmptyRequest request, ServerCallContext context)
        {
            bool isUserHavePlayer = false;
            Player player = _playerService.GetPlayer(_httpContext.GetUsername());

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
        public override Task<DeleteReply> DeletePlayer(EmptyRequest request, ServerCallContext context)
        {
            bool succes = _playerService.DeletePlayer(_httpContext.GetUsername());

            return Task.FromResult(new DeleteReply
            {
                IsDeleted = succes
            });
        }
        public override Task<GetPlayerReply> GetPlayer(EmptyRequest request, ServerCallContext context)
        {
            Player player = _playerService.GetPlayer(_httpContext.GetUsername());

            return Task.FromResult(new GetPlayerReply
            {
                Nick = player.Nick,
                Level = player.Level,
                Gold = player.Gold,
                Exp = player.Exp,
                ExpMax = player.ExpMax,
                Energy = player.Energy,
                EnergyMax = player.EnergyMax,
                Hp = player.Hp,
                HpMax = player.HpMax,
                Mana = player.Mana,
                ManaMax = player.ManaMax
            });
        }
    }
}