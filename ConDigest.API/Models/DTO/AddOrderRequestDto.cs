using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.DTO
{
    public class AddOrderRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public string? Note { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        public Guid? CouponId { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public List<AddOrderItemRequestDto> OrderDetail { get; set; }
    }
}
