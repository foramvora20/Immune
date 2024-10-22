using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Immune.Models
{
    public class VaccMembers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DOB   { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string City { get; set; }

        public string Dose1 { get; set; }
        public string Dose2 { get; set; }
        [Required]
        public string uId { get; set; }

    }
}
