using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystemProject.Model
{
    public class Register
    {
        [Required]  
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
