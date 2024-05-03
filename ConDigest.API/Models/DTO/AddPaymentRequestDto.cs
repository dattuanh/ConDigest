using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.DTO
{
    public class AddPaymentRequestDto
    {
        [Required]
        public string PaymentName { get; set; }

        [Required]
        public bool PaymentWay { get; set; }

        [Required]
        public string? PaymentMessage { get; set; }

        [Required]
        public Guid OrderId { get; set; }
    }
}
