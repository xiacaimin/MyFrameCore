using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyFrameCore.Web.Areas.WebSite.Controllers
{
    public class HomeController : Controller
    {
        [Area("WebSite")]
        public IActionResult Index()
        {
            return View();
        }
    }
}