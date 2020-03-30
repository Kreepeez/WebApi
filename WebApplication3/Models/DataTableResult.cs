using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class DataTableResult<BlogPost>
    {
       public List<Models.BlogPost> blogPosts { get; set; }

        public int postCount { get; set; }
    }
}
