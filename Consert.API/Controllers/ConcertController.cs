using ConcertBookingApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace Consert.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcertController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ConcertController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var concert = _context.Concerts.Find(id);
            if (concert == null)
            {
                return NotFound();
            }
            return Ok(concert);
        }
    }
}
