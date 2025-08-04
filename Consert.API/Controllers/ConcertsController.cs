using AutoMapper;
using Concert.Data.DTO;
using Concert.Data.Entity;
using Concert.Data.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<ConcertDto>>> GetConcerts()
        {
            var concerts = await _unitOfWork.Concerts.GetConcertsAsync(); 
            return Ok(_mapper.Map<IEnumerable<ConcertDto>>(concerts));
        }

        // ✅ Hämta en specifik konsert
        [HttpGet("{id}")]
        public async Task<ActionResult<ConcertDto>> GetConcert(string id)
        {
            var concert = await _unitOfWork.Concerts.GetConcertByIdAsync(id);

            if (concert == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ConcertDto>(concert));
        }

        // ✅ Uppdatera en konsert
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConcert(string id, [FromBody] ConcertDto concertDto)
        {
            if (string.IsNullOrEmpty(id) || concertDto == null)
            {
                return BadRequest("Invalid concert data.");
            }

            var existingConcert = await _unitOfWork.Concerts.GetConcertByIdAsync(id);
            if (existingConcert == null) return NotFound();

            // Map DTO to existing entity
            _mapper.Map(concertDto, existingConcert);
            existingConcert.ConcertId = id; // Ensure ID remains unchanged

            _unitOfWork.Concerts.UpdateConcert(existingConcert);
            await _unitOfWork.CompleteAsync(); 

            return Ok(_mapper.Map<ConcertDto>(existingConcert));
        }

        // ✅ Lägg till en ny konsert
        [HttpPost]
        public async Task<ActionResult<ConcertDto>> CreateConcert([FromBody] ConcertDto concertDto)
        {
            if (concertDto == null)
                return BadRequest("Invalid concert data.");

            // Validate required fields
            if (string.IsNullOrEmpty(concertDto.Title) || string.IsNullOrEmpty(concertDto.Description))
            {
                return BadRequest("Title and Description are required.");
            }

            var concert = _mapper.Map<ConcertEntity>(concertDto);
            concert.ConcertId = Guid.NewGuid().ToString(); // Generate new ID

            _unitOfWork.Concerts.AddConcert(concert);
            await _unitOfWork.CompleteAsync(); 

            return CreatedAtAction(nameof(GetConcert), new { id = concert.ConcertId }, _mapper.Map<ConcertDto>(concert));
        }

        // ✅ Ta bort en konsert
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcert(string id)
        {
            var concert = await _unitOfWork.Concerts.GetConcertByIdAsync(id);
            if (concert == null) return NotFound();

            _unitOfWork.Concerts.DeleteConcert(concert);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }


    }
}
