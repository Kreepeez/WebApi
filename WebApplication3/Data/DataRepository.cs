
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly WebApplication3Context _context;

        public DataRepository(WebApplication3Context context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }


        public async Task<BlogPostViewModel> GetViewModel(string slug)
        {
            var post = await Get(slug);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogPost, BlogPostViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            var dest = mapper.Map<BlogPost, BlogPostViewModel>(post);

            return dest;
        }

        public async Task<BlogPost> Get(string slug)
        {
            var post = await _context.BlogPosts.Include(x => x.BlogPostTags).ThenInclude(y => y.Tag).FirstOrDefaultAsync(x => x.Slug == slug);

            if (post == null)
            {

                return null;
               
            }

            post.TagList = post.BlogPostTags.Select(x => x.TagId).ToList();
            return post;
        }

        public async Task<IEnumerable<BlogPostViewModel>> Search(string tag)
        {

            var posts = await _context.BlogPosts.Include(x => x.BlogPostTags).ThenInclude(x => x.Tag).ToListAsync();

            if (!string.IsNullOrWhiteSpace(tag))
            {
                posts = posts.Where(x => x.BlogPostTags.Any(y => y.TagId == tag)).ToList();
            }

            foreach (var post in posts)
            {
                post.TagList = post.BlogPostTags?.Select(x => x.Tag.TagId).ToList();
            }

            var listOfPosts = Mapper.Map<IEnumerable<BlogPostViewModel>>(posts);


            return listOfPosts;
        }

       
        public async Task<BlogPostViewModel> Create(BlogPost post)
        {
          
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = post.CreatedAt;
            _context.BlogPosts.Add(post);

            if (post.TagList == null || post.TagList.Count == 0)
                goto noTags;

            post.BlogPostTags = new List<BlogPostTags>();

            var dbTags = _context.Tags.Select(x => x.TagId);

            foreach (var tag in post.TagList)
            {
                if (!dbTags.Contains(tag))
                {
                    _context.Tags.Add(new Tags() { TagId = tag });
                }

                BlogPostTags postTag = new BlogPostTags()
                {
                    TagId = tag,
                    Slug = post.Slug
                };

                post.BlogPostTags.Add(postTag);

            }
        noTags: await _context.SaveChangesAsync();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogPost, BlogPostViewModel>();
            }); 

            IMapper mapper = config.CreateMapper();
            
            var postVM = mapper.Map<BlogPostViewModel>(post);
            return postVM; 
        }


          public async Task<T> SaveAsync(T entity)
          {
              await _context.SaveChangesAsync();
              return entity;
          }

        public async Task<BlogPost> Update(BlogPost post)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<TagViewModel> GetAllTags()
        {
            var tags = await _context.Tags.ToListAsync();

            TagViewModel model = new TagViewModel()
            {
                Tags = new List<string>()
            };
            

            model.Tags = tags.Select(x => x.TagId).ToList();
            
            return model;
        }

    }
}
