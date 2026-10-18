using System.Diagnostics;
using group_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace group_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactsContext context;
        
        public HomeController(ContactsContext ctx) 
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var contacts = context.Contacts
                .Include(c => c.Category)
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToList();

            return View(contacts);
        }

    }
}
