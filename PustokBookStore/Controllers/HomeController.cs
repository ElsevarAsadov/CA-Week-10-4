using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PustokBookStore.Controllers
{
    public class HomeController : Controller
    {
    
        public IActionResult Index()
        {
            return View();
        }
    }
}