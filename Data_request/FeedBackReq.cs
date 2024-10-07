namespace intern_prj.Data_request
{
    public class FeedBackReq
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }
        public string? roleName { get; set; }

        public string? Description { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
