using Azure.Core;
using Microsoft.AspNetCore.Http;

namespace intern_prj.Helper
{
    public class ImageHepler
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageHepler(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor) {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string?> saveImage(IFormFile? image, string imageType)
        {
            if (image == null || image.Length == 0)
            {
                return null;
            }
            var fileName = Path.GetRandomFileName()
                + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(
                _environment.ContentRootPath, $"resource/images/{imageType}", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            var request = _httpContextAccessor.HttpContext?.Request;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/resource/images/{imageType}/{fileName}";
        }
    }
}
