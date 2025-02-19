using AutoMapper;
using Concert.Data.Entity;
using Concert.Data.Repository;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<Performance>>> GetPerformances()
        {
            var performances = await _unitOfWork.Performances.GetPerformancesAsync();
            return Ok(_mapper.Map<IEnumerable<Performance>>(performances));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Performance>> GetPerformance(string id)
        {
            var performance = await _unitOfWork.Performances.GetPerformanceByIdAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Performance>(performance));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerformance(string id, [FromBody] Performance performance)
        {
            if (id != performance.Id)
            {
                return BadRequest();
            }
            var existingPerformance = await _unitOfWork.Performances.GetPerformanceByIdAsync(id);
            if (existingPerformance == null) return NotFound();
            _unitOfWork.Performances.UpdatePerformance(performance);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddPerformance([FromBody] Performance performance)
        {
            _unitOfWork.Performances.AddPerformance(performance);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePerformance([FromBody] Performance performance)
        {
            _unitOfWork.Performances.DeletePerformance(performance);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPost]
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
