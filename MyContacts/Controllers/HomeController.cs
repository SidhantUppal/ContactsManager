using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.Models;
using System.Diagnostics;

namespace MyContacts.Controllers
{
    public class HomeController : Controller
    {
        ContactContext _context;
        public HomeController(ContactContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index(string searchString)
        {
            try
            {
                // Retrieve all contacts from the database, including their associated categories,
                // and order them by last name. This is done using eager loading to include the Category entity.
                var contacts = _context.Contacts.Include(c => c.Category).OrderBy(c => c.FirstName).ToList();

                // Check if a search string is provided to filter the contacts list.
                if (!String.IsNullOrEmpty(searchString))
                {

                    // Convert the search string to lowercase for case-insensitive comparison.
                    searchString = searchString.ToLower();

                    // Filter the contacts list based on the search string.
                    contacts = contacts.Where(s =>
                     s.FirstName.ToLower().Contains(searchString) || // Check if FirstName contains the search string
                     s.LastName.ToLower().Contains(searchString) || // Check if LastName contains the search string
                     s.PhoneNumber.Contains(searchString) ||  // Check if PhoneNumber contains the search string
                     s.Email.ToLower().Contains(searchString)  // Check if Email contains the search string
                 ).ToList();
                }
                return View(contacts);
            }
            catch (Exception ex)
            {
                return View(new List<Contact>());
            }
        }


    }
}
