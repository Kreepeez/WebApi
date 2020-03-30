
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    
    public class BlogService 
    {
        private readonly WebApplication3Context _context;
    
        public List<BlogPost> GetAllPosts() {

            List<BlogPost> allPosts = _context.BlogPosts.ToList();

            return allPosts;
        }
    } 
}
