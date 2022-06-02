using System.Collections.Generic;

namespace FiorelloTask.ViewModels
{
    public class Pagination<T>
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public List <T>  Items { get; set; }

        public Pagination(int pagecount,int currentpage,List<T>items)
        {
            PageCount=pagecount;
            CurrentPage=currentpage;
            Items=items;
        }
    }
}
