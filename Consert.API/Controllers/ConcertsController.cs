using AutoMapper;
using Concert.Data.Entity;
using Concert.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConcertsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ✅ Hämta alla konserter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConcertEntity>>> GetConcerts()
        {
            var concerts = await _unitOfWork.Concerts.GetConcertsAsync(); // Ändrat från Bookings
            return Ok(_mapper.Map<IEnumerable<ConcertEntity>>(concerts));
        }

        // ✅ Hämta en specifik konsert
        [HttpGet("{id}")]
        public async Task<ActionResult<ConcertEntity>> GetConcert(string id)
        {
            var concert = await _unitOfWork.Concerts.GetConcertByIdAsync(id); // Ändrat från GetBookingByIdAsync

            if (concert == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ConcertEntity>(concert));
        }

        // ✅ Uppdatera en konsert
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConcert(string id, [FromBody] ConcertEntity concert)
        {
            if (id != concert.ConcertId)
            {
                return BadRequest();
            }

            var existingConcert = await _unitOfWork.Concerts.GetConcertByIdAsync(id);
            if (existingConcert == null) return NotFound();

            _unitOfWork.Concerts.UpdateConcert(concert);
            await _unitOfWork.CompleteAsync(); // Sparar ändringen i databasen

            return Ok();
        }

        // ✅ Lägg till en ny konsert
        [HttpPost]
        public async Task<ActionResult<ConcertEntity>> CreateConcert([FromBody] ConcertEntity concert)
        {
            _unitOfWork.Concerts.AddConcert(concert); // Ändrat från AddBookingAsync
            await _unitOfWork.CompleteAsync(); // Sparar ändringen i databasen

            return CreatedAtAction(nameof(GetConcert), new { id = concert.ConcertId}, concert);
        }

        // ✅ Ta bort en konsert
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcert(string id)
        {
            var concert = await _unitOfWork.Concerts.GetConcertByIdAsync(id);
            if (concert == null) return NotFound();

            _unitOfWork.Concerts.DeleteConcert(concert); // Ändrat från DeleteBookingAsync
            await _unitOfWork.CompleteAsync(); // Sparar ändringen i databasen

            return Ok();
        }

       
    }
}
