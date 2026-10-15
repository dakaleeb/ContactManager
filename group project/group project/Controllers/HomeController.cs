using System.Diagnostics;
using group_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace group_project.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
