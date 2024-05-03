using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.DTO
{
    public class AddOrderItemRequestDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Subtotal { get; set; }
    }
}
