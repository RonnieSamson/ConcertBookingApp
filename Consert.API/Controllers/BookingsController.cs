using AutoMapper;
using Concert.Data.DTO;
using Concert.Data.Entity;
using Concert.Data.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookings = await _unitOfWork.Bookings.GetBookingsAsync();
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(string id)
        {
            var booking = await _unitOfWork.Bookings.GetBookingByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookingDto>(booking));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(string id, [FromBody] BookingDto bookingDto)
        {
            if (string.IsNullOrEmpty(id) || bookingDto == null)
            {
                return BadRequest("Invalid booking data.");
            }

            var existingBooking = await _unitOfWork.Bookings.GetBookingByIdAsync(id);
            if (existingBooking == null) return NotFound();

            // Map DTO to existing entity
            _mapper.Map(bookingDto, existingBooking);
            existingBooking.Id = id; // Ensure ID remains unchanged

            _unitOfWork.Bookings.UpdateBooking(existingBooking);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<BookingDto>(existingBooking));
        }

        [HttpPost("book")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null)
                return BadRequest("Invalid booking data.");

            // Validate required fields
            if (string.IsNullOrEmpty(bookingDto.PerformanceId) || 
                string.IsNullOrEmpty(bookingDto.CustomerName) || 
                string.IsNullOrEmpty(bookingDto.CustomerEmail))
            {
                return BadRequest("Performance ID, Customer Name, and Customer Email are required.");
            }

            // Validate email format
            if (!IsValidEmail(bookingDto.CustomerEmail))
            {
                return BadRequest("Invalid email format.");
            }

            var booking = _mapper.Map<Booking>(bookingDto);
            booking.Id = Guid.NewGuid().ToString(); // Generate new ID
            
            _unitOfWork.Bookings.AddBooking(booking);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, _mapper.Map<BookingDto>(booking));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
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

        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByUser(string userId)
        {
            var bookings = await _unitOfWork.Bookings.GetBookingsByUserIdAsync(userId);
            if (bookings == null || !bookings.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }

        [HttpGet("byEmail/{email}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByEmail(string email)
        {
            var bookings = await _unitOfWork.Bookings.GetBookingsByEmailAsync(email);
            if (bookings == null || !bookings.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }
    }
}
