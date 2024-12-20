using ConcertBookingApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ConcertBookingApp.API.Controllers
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

        // GET: api/concert
        [HttpGet]
        public IActionResult GetAll()
        {
            var concerts = _context.Concerts.ToList();
            return Ok(concerts);
        }

        // GET: api/concert/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var concert = _context.Concerts.Find(id);
            if (concert == null)
                return NotFound();

            return Ok(concert);
        }
    }
}