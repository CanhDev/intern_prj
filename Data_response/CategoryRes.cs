namespace intern_prj.Data_response
{
    public class CategoryRes
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public string? imageUrl { get; set; }
        public IFormFile? image { get; set; }
    }
}
