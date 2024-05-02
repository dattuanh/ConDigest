using ConDigest.API.Models.Domain;

namespace ConDigest.API.Models.DTO
{
    public class AddProductItemRequestDto
    {
        public string ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public double ProductPrice { get; set; }

        public int Stock { get; set; }

        public Guid CategoryId { get; set; }

        public Guid RestaurantId { get; set; }


        // Navigation properties
        public Category Category { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
