using intern_prj.Entities;

namespace intern_prj.Data_request
{
    public class CartReq
    {
        public int Id { get; set; }

        public int ProductTypeQuan { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? UserId { get; set; }

        public virtual ICollection<ItemCartReq> ItemCarts { get; set; } = new List<ItemCartReq>();

    }
}
