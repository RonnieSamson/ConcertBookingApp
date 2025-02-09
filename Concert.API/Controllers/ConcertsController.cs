using Microsoft.AspNetCore.Mvc;
using ConcertBookingApp.Data;
using ConcertBookingApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Concert.DTO;
using AutoMapper;


namespace Concert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ConcertsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Concerts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConcertDto>>> GetConcerts()
        {
            var concerts = await _context.Concerts.ToListAsync();

            // Mappa datan och spara den i en variabel för inspektion
            var mappedConcerts = _mapper.Map<IEnumerable<ConcertDto>>(concerts);

            // Debug-punkt: Här kan du inspektera mappedConcerts
            return Ok(mappedConcerts);
        }

        // GET: api/Concerts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConcertDto>> GetConcert(int id)
        {
            var concert = await _context.Concerts.FindAsync(id);

            if (concert == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ConcertDto>(concert));
        }

        // POST: api/Concerts
        [HttpPost]
        public async Task<ActionResult<ConcertDto>> PostConcert(ConcertDto concertDto)
        {
            var concert = _mapper.Map<ConcertBookingApp.Data.Entity.Concert>(concertDto);
            _context.Concerts.Add(concert);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConcert), new { id = concert.Id }, _mapper.Map<ConcertDto>(concert));
        }

        // PUT: api/Concerts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcert(int id, ConcertDto concertDto)
        {
            if (id != concertDto.Id)
            {
                return BadRequest();
            }

            var concert = await _context.Concerts.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }

            _mapper.Map(concertDto, concert);

            _context.Entry(concert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Concerts.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Concerts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcert(int id)
        {
            var concert = await _context.Concerts.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }

            _context.Concerts.Remove(concert);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
