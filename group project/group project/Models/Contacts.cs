using System.ComponentModel.DataAnnotations;

namespace group_project.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        [Required(ErrorMessage = "Please enter a First Name.")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a Last Name.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a Phone Number.")]
        public string Phone { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a Email.")]
        public string Email { get; set; } = string.Empty;
        public string? Organization { get; set; }
        public DateTime DateAdded { get; set; }
        [Required(ErrorMessage = "Please enter a Category ID")]
        [Range(1,1000000,ErrorMessage ="Please select a category.")]
        public int? CategoryID { get; set; }
        public Category Category { get; set; } = null;
        public string Slug => FirstName?.Replace(' ', '-').ToLower() + "-"+ LastName?.Replace(' ', '-').ToLower();
    }
}
