using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Data1;
using WEB_API.Model;

namespace WEB_API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Base")]  // call the controller by using controller name
    [ApiController]
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public BaseController(ApplicationDbContext DbContext)
        {
            _dbcontext = DbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<properties>>> GetProperties()
        {
            var result = _dbcontext.Properties.ToList();
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<properties>> Create(properties property)
        {
            _dbcontext.Properties.Add(property);
            await _dbcontext.SaveChangesAsync();

            // Return a response with the created entity
            return CreatedAtAction(nameof(GetProperties), new { id = property.id }, property);
        }
    }
}
