using System.ComponentModel.DataAnnotations;

namespace blazor.Models
{
    public class TimerInput
    {
        [Required]
        [Range(0, 99, ErrorMessage ="Please Enter Hour between 0-99")]
        public int Hours { get; set; }

        [Required]
        [Range(0, 60, ErrorMessage = "Please Enter Minutes between 0-60")]
        public int Minutes { get; set; }

        [Required]
        [Range(0, 60, ErrorMessage = "Please Enter Seconds between 0-60")]
        public int Seconds { get; set; }
    }
}
