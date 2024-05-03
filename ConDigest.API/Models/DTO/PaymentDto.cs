namespace ConDigest.API.Models.DTO
{
    public class PaymentDto
    {
        public Guid Id { get; set; }

        public string PaymentName { get; set; }

        public bool PaymentWay { get; set; }

        public string? PaymentMessage { get; set; }

        public Guid OrderId { get; set; }
    }
}
