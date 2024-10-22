using System.ComponentModel.DataAnnotations;

namespace Immune.Models
{
    public class Vaccine
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string city { get; set; }
        [Required]
        public string Dose1 { get; set; }
        [Required]
        public string Dose2 { get; set; }
        [Required]
        public string Date { get; set; }
    }
}
