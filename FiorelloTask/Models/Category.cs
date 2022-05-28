using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiorelloTask.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Don't empty"), MaxLength(10, ErrorMessage = "Maxlength 10den cox olmamalidi")]
       
        public string Name { get; set; }


        [MaxLength(30, ErrorMessage = "Maxlength 30den cox olmamalidi")]
        public string Desc { get; set; }
        public virtual IEnumerable<Product>Products { get; set; }
    }
}
