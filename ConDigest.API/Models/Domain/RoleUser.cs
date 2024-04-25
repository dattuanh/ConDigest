using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace ConDigest.API.Models.Domain
{
    public class RoleUser
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }

        // Navigation properties
        public Roles Roles { get; set; }

        public User User { get; set; }
    }
}
