using Microsoft.AspNetCore.Http;

namespace Server.Services
{
    public class HttpContext
    {
        IHttpContextAccessor _httpContextAccessor;
        public HttpContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst("preferred_username").Value;
        }
    }
}
