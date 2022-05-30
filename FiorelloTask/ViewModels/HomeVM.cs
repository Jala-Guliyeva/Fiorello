using FiorelloTask.Models;
using System.Collections;
using System.Collections.Generic;

namespace FiorelloTask.ViewModels
{
    public class HomeVM
    {
        public IEnumerable <Slider> sliders { get; set; }
        public PageIntro pageIntro { get; set; }
        public IEnumerable<Category> categories { get; set; }
       
    }
}
