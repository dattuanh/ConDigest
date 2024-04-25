using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.Domain
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Rating { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ProductItem ProductItem { get; set; }
    }
}
