using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.DTO.CouponDTOs
{
    public class AddCouponRequestDto
    {
        public string? CouponCode { get; set; }

        public string? CouponName { get; set; }

        [Required(ErrorMessage = "From Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "From Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [DateRange(nameof(FromDate), ErrorMessage = "From Date must be before To Date")]
        public DateTime ToDate { get; set; }
    }
}
