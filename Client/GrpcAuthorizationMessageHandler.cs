using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Client
{
    public class GrpcAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public GrpcAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
        {
            ConfigureHandler(new[] {"https://localhost:5001"});
        }
    }
}