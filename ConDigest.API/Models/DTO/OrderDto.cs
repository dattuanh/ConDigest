using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public string? Note { get; set; }

        public double TotalAmount { get; set; }

        public Guid? CouponId { get; set; }

        public string? Status { get; set; }

        public string? PaymentMethod { get; set; }
    }
}
