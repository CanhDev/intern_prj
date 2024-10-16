namespace intern_prj.Data_request
{
    public class ItemCartReq
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? CartId { get; set; }

        public int? Quantity { get; set; }

        public decimal Price { get; set; }
        public productReq? Product { get; set; }
    }
}
