using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public string CategoryName { get; set; }
    }
}
