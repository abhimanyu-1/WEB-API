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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, properties updatedProperty)
        {
            if (id != updatedProperty.id)
            {
                return BadRequest("Property ID mismatch");
            }

            var existingProperty = await _dbcontext.Properties.FindAsync(id);
            if (existingProperty == null)
            {
                return NotFound("Property not found");
            }

            // Update the existing property's properties with the values from the updatedProperty
            existingProperty.Name = updatedProperty.Name;
            existingProperty.Description = updatedProperty.Description;
            // Update other properties as needed

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbcontext.Properties.Any(e => e.id == id))
                {
                    return NotFound("Property not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProperty = await _dbcontext.Properties.FindAsync(id);
            if (existingProperty == null)
            {
                return NotFound("Property not found");
            }

            _dbcontext.Properties.Remove(existingProperty);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }
    }
}