using ConDigest.API.Data;
using ConDigest.API.Models.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ConDigest.API.Repositories
{
    public class NewsImageRepository : INewsImageRepository
    {
        private readonly ConDigestDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ConDigestDBContext conDigestDBContext)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            this._context = conDigestDBContext;
        }
        public async Task<NewsImage> UploadNewImage(NewsImage image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
                $"{image.NewsImageName}{image.FileExtension}");

            // Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);

            await image.File.CopyToAsync(stream);


            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.NewsImageName}{image.FileExtension}";
            image.FilePath = urlFilePath;


            // Add Image to the Images table
            await _context.NewsImages.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
