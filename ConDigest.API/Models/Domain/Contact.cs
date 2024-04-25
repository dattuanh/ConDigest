using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.Domain
{
    public class Contact
    {
        public Guid Id { get; set; }

        public string ContactName { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactMessage { get; set; }
    }
}
