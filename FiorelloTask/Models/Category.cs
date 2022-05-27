using System.Collections;
using System.Collections.Generic;

namespace FiorelloTask.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public IEnumerable<Product>Products { get; set; }
    }
}
