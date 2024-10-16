namespace intern_prj.Data_request
{
    public class UserReq
    {
        public string? Id { get; set; } = null;
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        
        public string? PhoneNum { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string? avatarUrl { get; set; } = null;
    }
}
