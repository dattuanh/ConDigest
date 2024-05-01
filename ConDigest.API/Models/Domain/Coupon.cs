using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.Domain
{
    public class Coupon
    {
        public Guid Id { get; set; }

        public string CouponCode { get; set; }

        public string CouponName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ToDate { get; set; }
    }
}
