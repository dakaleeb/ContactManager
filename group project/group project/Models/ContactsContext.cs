using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace group_project.Models
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Friend" },
                new Category { CategoryId = 2, CategoryName = "Family" },
                new Category { CategoryId = 3, CategoryName = "Stranger" }
            );
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Kaleb",
                    LastName = "Hilla",
                    Phone = "(123) 456-7890",
                    Email = "Kaleb.hilla@southeasttech.edu",
                    CategoryID = 1
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Kyla",
                    LastName = "Hilla",
                    Phone = "(123) 456-1111",
                    Email = "Kyla.hilla@example.com",
                    CategoryID = 2
                },
                new Contact
                {
                    ContactId = 3,
                    FirstName = "Nate",
                    LastName = "Dogg",
                    Phone = "(123) 444-4444",
                    Email = "NateDogg@southeasttech.edu",
                    CategoryID = 3
                }
            );
        }
    }
}
