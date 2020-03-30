using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly WebApplication3Context _context;
        private readonly IDataRepository<Tags> _repo;

        public TagsController(WebApplication3Context context, IDataRepository<Tags> dataRepository)
        {
            _context = context;
            _repo = dataRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagViewModel>>> GetTags()
        {
            var tags = await _repo.GetAllTags();
            return Ok(tags);
        }
    }
       
}
