using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public interface IDataRepository<T> where T : class 
    { 
        void Add(T entity);
        Task<BlogPost> Update(BlogPost post);
        void Delete(T entity);
        Task<T> SaveAsync(T entity);
        Task<BlogPostViewModel> GetViewModel(string slug);
        Task<IEnumerable<BlogPostViewModel>> Search(string tag);
        Task<BlogPostViewModel> Create(BlogPost post);
        Task<BlogPost> Get(string slug);
        Task<TagViewModel> GetAllTags();
    }
}
