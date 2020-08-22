using System.Threading.Tasks;
using Greet;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.Services;
using Server.Services.Model;

namespace Server
{
    [Authorize]
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly BookService _bookService;
        private readonly PlayerService _playerService;

        public GreeterService(ILogger<GreeterService> logger, BookService bookService,PlayerService playerService)
        {
            _logger = logger;
            _bookService = bookService;
            _playerService = playerService;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Book book = _bookService.CreateBook("Pan Lodowego Ogrodu", "Jarosław Grzędowicz");
            return Task.FromResult(new HelloReply
            {
                Message = "Dodano pomyślnie"
            });
        }
    }
}