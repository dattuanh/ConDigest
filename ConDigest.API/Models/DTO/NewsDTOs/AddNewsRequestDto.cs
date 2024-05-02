using System.ComponentModel.DataAnnotations.Schema;

namespace ConDigest.API.Models.DTO.NewsDTOs
{
    public class AddNewsRequestDto
    {
        public string Title { get; set; }

        public string? Content { get; set; }

        public string CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
    }
}
