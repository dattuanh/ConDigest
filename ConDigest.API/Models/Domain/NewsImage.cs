using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.Domain
{
    public class NewsImage
    {
        public Guid Id { get; set; }

        public string NewsImageName { get; set; }

        public string? NewsImageDescription { get; set; }

        public Guid NewsId { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }
        // Navigation property
        public News News { get; set; }
    }
}
