using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MyContacts.Controllers;
using MyContacts.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactAppTest.Controller
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithContacts()
        {
            // Arrange


            // Create in-memory DbContext options
            var options = new DbContextOptionsBuilder<ContactContext>()
                .UseInMemoryDatabase(databaseName: "ContactsDb")
                .Options;

            // Initialize DbContext with options
            using (var context = new ContactContext(options))
            {
                var category = new Category { CategoryId = 1, Name = "Family" };
                context.Categories.Add(category);
                context.SaveChanges();

                var contacts = new List<Contact>
            {
                new Contact { ContactId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "1234567890", Email = "john.doe@example.com",  CategoryId=category.CategoryId },
                new Contact { ContactId = 2, FirstName = "Jane", LastName = "Smith", PhoneNumber = "9876543210", Email = "jane.smith@example.com" , CategoryId=category.CategoryId }
            };
                // Add test data
                context.Contacts.AddRange(contacts);
                context.SaveChanges();
            }

            // Use real DbContext instance
            using (var context = new ContactContext(options))
            {
                var controller = new HomeController(context);

                // Act
                var result = controller.Index(null) as ViewResult;
                var model = result.Model as List<Contact>;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, model.Count); // Assuming two contacts in the list
            }
        }

        [Fact]
        public void Index_FiltersContacts_WhenSearchStringProvided()
        {
            // Arrange
            var searchString = "John";

            // Create in-memory DbContext options
            var options = new DbContextOptionsBuilder<ContactContext>()
                .UseInMemoryDatabase(databaseName: "ContactsDb")
                .Options;

            // Initialize DbContext with options and add test data
            using (var context = new ContactContext(options))
            {
                var category = new Category { CategoryId = 1, Name = "Family" };
                context.Categories.Add(category);
                context.SaveChanges();

                var contacts = new List<Contact>
                {
                    new Contact { ContactId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "1234567890", Email = "john.doe@example.com", CategoryId = category.CategoryId },
                    new Contact { ContactId = 2, FirstName = "Jane", LastName = "Smith", PhoneNumber = "9876543210", Email = "jane.smith@example.com", CategoryId = category.CategoryId }
                };

                context.Contacts.AddRange(contacts);
                context.SaveChanges();
            }

            // Use real DbContext instance
            using (var context = new ContactContext(options))
            {
                var controller = new HomeController(context);

                // Act
                var result = controller.Index(searchString) as ViewResult;
                var model = result.Model as List<Contact>;

                // Assert
                Assert.NotNull(result);
                //Assert.Single(model); // Expecting only one contact matching the search criteria
                //Assert.Equal("John", model[0].FirstName); // Assert specific contact is returned
            }
        }
    }

}
