using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiorelloTask.Models
{

    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }

        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
