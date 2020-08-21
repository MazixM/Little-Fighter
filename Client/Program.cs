using System.Net.Http;
using System.Threading.Tasks;
using Greet;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            
            builder.Services.AddScoped<GrpcAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp =>
            {
                var authorizationMessageHandler = 
                    sp.GetRequiredService<GrpcAuthorizationMessageHandler>();
                authorizationMessageHandler.InnerHandler = new HttpClientHandler();
                var grpcWebHandler = 
                    new GrpcWebHandler(GrpcWebMode.GrpcWeb, authorizationMessageHandler);
                var channel = GrpcChannel.ForAddress("https://localhost:5001", 
                    new GrpcChannelOptions { HttpHandler = grpcWebHandler });

                return new Greeter.GreeterClient(channel);
            });

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Local", options.ProviderOptions);
                options.ProviderOptions.ResponseType = "code";
            });
            await builder.Build().RunAsync();
        }
    }
}
