using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Concert.Data.Entity;
using Concert.Data.Repository;
using AutoMapper;

namespace Concert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var bookings = await _unitOfWork.Bookings.GetBookingsAsync();
            return Ok(_mapper.Map<IEnumerable<Booking>>(bookings));
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(string id)
        {
            var booking = await _unitOfWork.Bookings.GetBookingByIdAsync(id); 

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Booking>(booking));
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(string id, [FromBody] Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            var existingBooking = await _unitOfWork.Bookings.GetBookingByIdAsync(id);
            if (existingBooking == null) return NotFound();

            _unitOfWork.Bookings.UpdateBooking(booking);
            await _unitOfWork.CompleteAsync(); 

            return Ok();
        }

        
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] Booking booking)
        {
            _unitOfWork.Bookings.AddBooking(booking); 
            await _unitOfWork.CompleteAsync(); 

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            var booking = await _unitOfWork.Bookings.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            _unitOfWork.Bookings.DeleteBooking(booking); 
            await _unitOfWork.CompleteAsync(); 

            return Ok();
        }

        
       
    }
}
