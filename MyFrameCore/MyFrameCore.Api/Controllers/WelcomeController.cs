using Microsoft.AspNetCore.Mvc;

namespace MyFrameCore.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Welcome")]
    public class WelcomeController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "欢迎访问API";
        }
    }
}