using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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