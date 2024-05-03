using ConDigest.API.Models.Domain;

namespace ConDigest.API.Models.DTO.NewsImageDTOs
{
    public class NewsImageUploadRequestDto
    {
        public string NewsImageName { get; set; }

        public string? NewsImageDescription { get; set; }

        public Guid NewsId { get; set; }

        public IFormFile File { get; set; }
        
        // Navigation property
        public News News { get; set; }
    }
}