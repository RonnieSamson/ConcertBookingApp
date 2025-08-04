using AutoMapper;
using Concert.Data.DTO;
using Concert.Data.Entity;
using Concert.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Consert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PerformancesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceDto>>> GetPerformances()
        {
            var performances = await _unitOfWork.Performances.GetPerformancesAsync();
            return Ok(_mapper.Map<IEnumerable<PerformanceDto>>(performances));
        }

        [HttpGet("byConcert/{concertId}")]
        public async Task<ActionResult<IEnumerable<PerformanceDto>>> GetPerformancesByConcertId(string concertId)
        {
            var performances = await _unitOfWork.Performances.GetPerformancesByConcertIdAsync(concertId);
            if (performances == null || !performances.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PerformanceDto>>(performances));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceDto>> GetPerformance(string id)
        {
            var performance = await _unitOfWork.Performances.GetPerformanceByIdAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PerformanceDto>(performance));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerformance(string id, [FromBody] PerformanceDto performanceDto)
        {
            if (string.IsNullOrEmpty(id) || performanceDto == null)
            {
                return BadRequest("Invalid performance data.");
            }

            var existingPerformance = await _unitOfWork.Performances.GetPerformanceByIdAsync(id);
            if (existingPerformance == null) return NotFound();

            // Map DTO to existing entity
            _mapper.Map(performanceDto, existingPerformance);
            existingPerformance.Id = id; // Ensure ID remains unchanged

            _unitOfWork.Performances.UpdatePerformance(existingPerformance);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<PerformanceDto>(existingPerformance));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPerformance([FromBody] PerformanceDto performanceDto)
        {
            if (performanceDto == null)
                return BadRequest("Invalid performance data.");

            // Validate required fields
            if (string.IsNullOrEmpty(performanceDto.Location) || string.IsNullOrEmpty(performanceDto.ConcertId))
            {
                return BadRequest("Location and ConcertId are required.");
            }

            var performance = _mapper.Map<Performance>(performanceDto);
            performance.Id = Guid.NewGuid().ToString(); // Generate new ID

            _unitOfWork.Performances.AddPerformance(performance);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetPerformance), new { id = performance.Id }, _mapper.Map<PerformanceDto>(performance));
        }

        [HttpDelete("DeleteByObject")]
        public async Task<IActionResult> DeletePerformance([FromBody] PerformanceDto performanceDto)
        {
            if (performanceDto == null || string.IsNullOrEmpty(performanceDto.Id))
                return BadRequest("Invalid performance data.");

            var performance = await _unitOfWork.Performances.GetPerformanceByIdAsync(performanceDto.Id);
            if (performance == null) return NotFound();

            _unitOfWork.Performances.DeletePerformance(performance);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformance(string id)
        {
            var performance = await _unitOfWork.Performances.GetPerformanceByIdAsync(id);
            if (performance == null) return NotFound();
            _unitOfWork.Performances.DeletePerformance(performance);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}