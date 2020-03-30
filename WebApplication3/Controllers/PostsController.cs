using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Helper;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly WebApplication3Context _context;
        private readonly IDataRepository<BlogPost> _repo;

        public PostsController(WebApplication3Context context, IDataRepository<BlogPost> repo)
        {
            _context = context;
            _repo = repo;
           
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostViewModel>>> GetPosts(string tag)
        {
            var posts = await _repo.Search(tag);
            return Ok(new { blogPosts = posts, postsCount = posts.Count() });
        }

       
        [HttpGet("{slug}")]
        public async Task<ActionResult<BlogPostViewModel>> GetPost(string slug)
        {
            var post = await _repo.GetViewModel(slug);
            if (post == null)
                return NotFound();
            return Ok(new { blogPost = post });
        }

        
          [HttpPut("{slug}")]
          [Consumes("application/json")]
          public async Task<ActionResult<BlogPostViewModel>> Update([FromRoute]string slug, [FromBody] BlogPost newPost)
          {
               System.Diagnostics.Debug.WriteLine("pokemoni", newPost);
               var existingPost = await _repo.Get(slug);

              if (existingPost == null)
              {
                  return NotFound();
              }

            if (newPost.Title == null)
            {
                newPost.Title = existingPost.Title;

                string s = existingPost.Title.ToLower().Normalize(NormalizationForm.FormD);
                s = Regex.Replace(s, @"\s+", "-");
                s = Regex.Replace(s, "[^0-9A-Za-z -]", "");
                s = RemoveAccented.RemoveDiacritics(s);
                newPost.Slug = s;
            }
            else {
                string s = newPost.Title.ToLower().Normalize(NormalizationForm.FormD);
                s = Regex.Replace(s, @"\s+", "-");
                s = Regex.Replace(s, "[^0-9A-Za-z -]", "");
                s = RemoveAccented.RemoveDiacritics(s);
                newPost.Slug = s;
            }

            if (newPost.Description == null)
                  newPost.Description = existingPost.Description;

              if (newPost.Body == null)
                  newPost.Body = existingPost.Body;

              newPost.CreatedAt = existingPost.CreatedAt;
              newPost.UpdatedAt = DateTime.Now;
              
               _context.Remove(existingPost);

               _context.Add(newPost);

              var updatedPost = await _repo.SaveAsync(newPost);
               System.Diagnostics.Debug.WriteLine("pokemoni", newPost.ToString());
               return Ok(updatedPost);
          }

        [HttpPost]
        [Consumes("application/json; charset=utf-8")]
        public async Task<ActionResult<BlogPostWrapper>> Create([FromBody] BlogPost post)
        {
               string s = post.Title.ToLower().Normalize(NormalizationForm.FormD);
               s = Regex.Replace(s, @"\s+", "-");
               s = Regex.Replace(s, "[^0-9A-Za-z -]", "");
               s = RemoveAccented.RemoveDiacritics(s);
               post.Slug = s;    
             var createdPost = await _repo.Create(post);
             return Ok(new { blogPost = createdPost });
        }


        // DELETE: api/BlogPosts/5
        [HttpDelete("{slug}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] string slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogPost = await _context.BlogPosts.FindAsync(slug);
            if (blogPost == null)
            {
                return NotFound();
            }

            _repo.Delete(blogPost);
            _ = await _repo.SaveAsync(blogPost);

            return NoContent();
        }

        private bool BlogPostExists(string id)
        {
            return _context.BlogPosts.Any(e => e.Slug == id);
        }
    }
}
