using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.Entity.DTO
{
   public class PageResult<T> where T:class,new()
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
