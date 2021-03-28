using System.ComponentModel.DataAnnotations;

namespace Mutant.Models.DTOs
{
    public class SequenceDTO
    {
        [Required(ErrorMessage = "A DNA sequence is required")]
        public string[] DNA { get; set; }
    }
}
