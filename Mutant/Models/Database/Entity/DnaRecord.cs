using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mutant.Models.Database.Entity
{
    public class DnaRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string DNA { get; set; }

        [Required]
        public bool IsMutant { get; set; }
    }
}
