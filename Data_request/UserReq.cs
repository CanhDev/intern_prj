namespace intern_prj.Data_request
{
    public class UserReq
    {
        public string fname { get; set; } = null!;
        public string lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string? avatarUrl { get; set; } = null;
    }
}
