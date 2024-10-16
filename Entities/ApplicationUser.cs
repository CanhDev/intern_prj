using Microsoft.AspNetCore.Identity;

namespace intern_prj.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNum {  get; set; }
        public string? Address {  get; set; }
        public string? avatarUrl {  get; set; }

        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
