using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{

   
 // [JsonObject]
    public class BlogPostWrapper
    {
        [JsonProperty("blogPost")]
        public BlogPost BlogPost;

        
        public BlogPost GetBlogPost()
        {
            return BlogPost;
        }

        public void SetBlogPost(BlogPost value)
        {
            BlogPost = value;
        }
         
        public List<BlogPost> BlogPosts;

        public List<BlogPost> GetBlogPosts()
        {
            return BlogPosts;
        }

        public void SetBlogPosts(List<BlogPost> value)
        {
            BlogPosts = value;
        }

        public int postsCount { get; set; }

      //  [JsonConstructor]
        public BlogPostWrapper() {
           
        }

       
    }
}
