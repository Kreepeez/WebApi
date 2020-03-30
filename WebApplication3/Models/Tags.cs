using FluentNHibernate.Conventions.Inspections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Tags
    {
        [Key]
        public string TagId { get; set; }
        public List<BlogPostTags> BlogPostTags { get; set; }
        public Tags() {
           
        }
      
    }
}
