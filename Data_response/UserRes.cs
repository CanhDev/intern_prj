namespace intern_prj.Data_response
{
    public class UserRes
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? avatarUrl { get; set; } = null;
        public IFormFile avatarImage { get; set; } = null!;
    }
}
