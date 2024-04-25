using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }

        public string? Note { get; set; }

        public double TotalAmount { get; set; }

        public Guid? CouponId { get; set; }

        public string? Status { get; set; }

        public string? PaymentMethod { get; set; }

        // Navigation properties
        public Coupon Coupon { get; set; }
        public User User { get; set; }
    }
}
