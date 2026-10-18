using System.Diagnostics;
using group_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace group_project.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactsContext context;

        public ContactController(ContactsContext ctx)
        {
            context = ctx;
        }

        private void LoadCategories()
        {
            ViewBag.Categories = context.Categories
                .OrderBy(c => c.CategoryName)
                .ToList();
        }

        [HttpGet]
        public IActionResult Details(int id, string? slug)
        {
            var contact = context.Contacts
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ContactId == id);

            if (contact == null)
                return NotFound();

            return View(contact);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            LoadCategories();

            return View("Edit", new Contact());
        }

        [HttpGet]
        public IActionResult Edit(int id, string? slug)
        {
            var contact = context.Contacts
                .FirstOrDefault(c => c.ContactId == id);

            if (contact == null)
                return NotFound();

            ViewBag.Action = "Edit";
            LoadCategories();

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            bool isNewContact = contact.ContactId == 0;

            if (ModelState.IsValid)
            {
                if (isNewContact)
                {
                    contact.DateAdded = DateTime.Now;
                    context.Contacts.Add(contact);
                }
                else
                {
                    var existingContact = context.Contacts
                        .AsNoTracking()
                        .FirstOrDefault(
                            c => c.ContactId == contact.ContactId);

                    if (existingContact == null)
                        return NotFound();

                    // Keep the original Date Added value
                    contact.DateAdded = existingContact.DateAdded;

                    context.Contacts.Update(contact);
                }

                context.SaveChanges();

                if (isNewContact)
                    return RedirectToAction("Index", "Home");

                return RedirectToAction(
                    "Details",
                    new { id = contact.ContactId, slug = contact.Slug });
            }

            ViewBag.Action = isNewContact ? "Add" : "Edit";
            LoadCategories();

            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int id, string? slug)
        {
            var contact = context.Contacts
                .Include(c => c.Category)
                .FirstOrDefault(c => c.ContactId == id);

            if (contact == null)
                return NotFound();

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int contactId)
        {
            var contact = context.Contacts.Find(contactId);

            if (contact == null)
                return NotFound();

            context.Contacts.Remove(contact);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
