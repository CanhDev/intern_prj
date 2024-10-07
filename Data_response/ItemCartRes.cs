namespace intern_prj.Data_response
{
    public class ItemCartRes
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? CartId { get; set; }

        public int? OrderDetailId { get; set; }

        public int? Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
