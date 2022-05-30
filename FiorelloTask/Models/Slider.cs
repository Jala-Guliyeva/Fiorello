using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiorelloTask.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        //iformfile-bazaya dusmediyi ucun sadece sekili goturmekucun istf olunur
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
