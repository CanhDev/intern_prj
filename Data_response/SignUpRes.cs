using System.ComponentModel.DataAnnotations;

namespace intern_prj.Data_response
{
    public class SignUpRes
    {
        [Required]
        public string fname { get; set; } = null!;
        [Required]
        public string lname { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
