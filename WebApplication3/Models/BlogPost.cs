using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    [JsonObject]

    public class BlogPost
    {
        [Key]  
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    
        [NotMapped]
        public List<string> TagList { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }    

        public List<BlogPostTags> BlogPostTags { get; set; }
        
        public BlogPost()
        {
        }

    }
}
