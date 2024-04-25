using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.Domain
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
    }
}
