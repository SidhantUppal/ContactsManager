using Microsoft.EntityFrameworkCore;

namespace MyContacts.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, Name = "Family" },
              new Category { CategoryId = 2, Name = "Friend" },
              new Category { CategoryId = 3, Name = "Work" }
            );

            modelBuilder.Entity<Contact>().HasData(
              new Contact
                  {
                     ContactId = 1,
                     FirstName = "John",
                     LastName = "Doe",
                     PhoneNumber = "9875664346",
                     Email = "jon@gmail.com",
                     CategoryId = 1,
                     Address="Mohai"
                     },
              new Contact
              {
                  ContactId = 2,
                  FirstName = "David",
                  LastName = "Boon",
                  PhoneNumber = "9876545678",
                  Email = "david@gmail.com",
                  CategoryId = 1,
                  Address = "Chandigarh"
              },
              new Contact
              {
                  ContactId = 3,
                  FirstName = "Andrew",
                  LastName = "Blake",
                  PhoneNumber = "9876987654",
                  Email = "andrew@gmail.com",
                  CategoryId = 2,

              }
         );
        }
    }   
   
}
