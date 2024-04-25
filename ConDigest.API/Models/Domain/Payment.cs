using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }

        public string PaymentName { get; set; }

        public bool PaymentWay { get; set; }

        public string? PaymentMessage { get; set; }

        public Guid OrderId { get; set; }

        // Navigation property
        public Order Order { get; set; }
    }
}
