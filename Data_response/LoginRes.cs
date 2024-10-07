using System.ComponentModel.DataAnnotations;

namespace intern_prj.Data_response
{
    public class LoginRes
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
