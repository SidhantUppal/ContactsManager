using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.Models;

namespace MyContacts.Controllers
{
    public class ContactController : Controller
    {
        ContactContext _ctx;

        public ContactController(ContactContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Retrieve all categories from the database and store them in the ViewBag.
            // This is to populate a dropdown list or similar UI element for selecting a category.
            ViewBag.Categories = _ctx.Categories.ToList();
            ViewBag.Action = "Add";

            // Return the "AddEdit" view, passing a new, empty Contact object.
            // This is to initialize the form for adding a new contact.
            return View("AddEdit", new Contact());
        }

        [HttpPost]
        public IActionResult Add(Contact contact)
        {
            // Check if the model state is valid (i.e., all data annotations are satisfied).
            // If valid, proceed to add the contact to the database.
            if (ModelState.IsValid)
            {
                // Set the current date and time as the DateAdded property of the contact.
                contact.DateAdded = DateTime.Now;

                // Add the contact to the Contacts table in the context.
                _ctx.Contacts.Add(contact);

                // Save changes to the database to persist the new contact.
                _ctx.SaveChanges();

                // Redirect to the Index action of the Home controller after successfully adding the contact.
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = _ctx.Categories.ToList();
            return View("AddEdit", contact);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Find the contact in the database context by its unique identifier (ID).
            var contact = _ctx.Contacts.Find(id);

            // Return the view with the contact model.
            // This view will typically prompt the user for confirmation before deleting.
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Contact contact)
        {
            // Remove the specified contact from the database context.
            _ctx.Contacts.Remove(contact);

            // Save changes to the database to persist the deletion.
            _ctx.SaveChanges();

            // Redirect to the Index action of the Home controller to refresh the contact list view.
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EditContact(int id)
        {
            // Retrieve the contact from the database using the provided ID.
            var contact = _ctx.Contacts.Find(id);

            // If the contact is not found (null), return a 404 Not Found result.
            if (contact == null)
            {
                return NotFound();
            }

            // Populate ViewBag with categories to be used in a dropdown or similar control in the view.
            ViewBag.Categories = _ctx.Categories.ToList();

            // Add the contact's address to ViewBag to be used in the view, especially if it's used in maps or address fields.
            ViewBag.Address = contact.Address;

            return PartialView("_EditContactPartial", contact);
        }

        [HttpPost]
        public IActionResult EditContact(Contact contact)
        {
            // Check if the model is valid according to the data annotations and validation rules.
            if (ModelState.IsValid)
            {
                // Update the contact in the database.
                _ctx.Contacts.Update(contact);

                // Save the changes to the database.
                _ctx.SaveChanges();

                // If AJAX request, return JSON success message
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    // Return a JSON response indicating success.
                    return Json(new { success = true });
                }
                // If not an AJAX request, redirect to the Index action of the Home controller.
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Categories = _ctx.Categories.ToList();

            // If AJAX request, return the form with validation errors
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_EditContactPartial", contact);
            }

            return View(contact);
        }

    }
}
