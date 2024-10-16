namespace intern_prj.Data_response
{
    public class UserRes
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? PhoneNum { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? AvatarUrl { get; set; } = null;
        public IFormFile? AvatarImage { get; set; } = null!;
    }
}
