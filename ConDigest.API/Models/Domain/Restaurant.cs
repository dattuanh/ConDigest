using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.Domain
{
    public class Restaurant
    {
        public Guid Id { get; set; }

        public string RestaurantName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
