using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.DTO.CouponDTOs
{
    public class CouponDto
    {
        public Guid Id { get; set; }

        public string? CouponCode { get; set; }

        public string? CouponName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ToDate { get; set; }
    }
}
