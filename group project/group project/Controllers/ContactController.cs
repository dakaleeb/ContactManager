using Microsoft.AspNetCore.Mvc;
using group_project.Models;

namespace group_project.Controllers
{
    public class ContactController : Controller
    {
        private ContactsContext context { get; set; }
        public ContactController(ContactsContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Action = "Details";
            var contact = context.Contacts.Find(id);
            return View(contact);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Categories = context.Categories.OrderBy(g => g.CategoryName).ToList();
            return View("Edit", new Contact());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Categories = context.Categories.OrderBy(g => g.CategoryName).ToList();
            var contact = context.Contacts.Find(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.ContactId == 0)
                    context.Contacts.Add(contact);
                else
                    context.Contacts.Update(contact);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (contact.ContactId == 0) ? "Add" : "Edit";
                ViewBag.Categories = context.Categories.OrderBy(g => g.CategoryName).ToList();
                return View(contact);
            } 
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = context.Contacts.Find(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Contact contact)
        {
            context.Contacts.Remove(contact);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
