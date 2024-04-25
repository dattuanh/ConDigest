using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.Domain
{
    public class OrderDetail
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public double Subtotal { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public ProductItem ProductItem { get; set; }
    }
}
