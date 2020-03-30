using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class BlogPostTags
    {
        [ForeignKey("BlogPost")]
        public string Slug { get; set; }

        public BlogPost BlogPost { get; set; }

        [ForeignKey("Tags")]
        public string TagId { get; set; }

        public Tags Tag { get; set; }
    }
}
