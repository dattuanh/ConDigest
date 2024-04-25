using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.Domain
{
    public class ProductItemImage
    {
        public Guid Id { get; set; }

        public string ProductImageName { get; set; }

        public string? ProductImageDescription { get; set; }

        public Guid ProductId { get; set; }

        // Navigation property
        public ProductItem ProductItem { get; set; }
    }
}
