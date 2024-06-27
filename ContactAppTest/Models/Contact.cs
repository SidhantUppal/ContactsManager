using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyContacts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppTest.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? CategoryId { get; set; }

        public string? Address { get; set; }

        public Category Category { get; set; } = null!;

        public DateTime DateAdded { get; set; }

    }
}
