namespace intern_prj.Data_request
{
    public class CategoryReq
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? imageUrl { get; set; }

        public string? Description { get; set; }
    }
}
