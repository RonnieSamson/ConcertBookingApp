using AutoMapper;
using Concert.Data.DTO;
using Concert.Data.Entity;
using Concert.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Concert.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ✅ BEHÖVER VI: För autentisering via email
        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }

        // ✅ BEHÖVER VI: För registrering
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
                return BadRequest("Email and Password are required.");

            // Check if user already exists
            var existingUser = await _unitOfWork.Users.GetUserByEmailAsync(userDto.Email);
            if (existingUser != null)
                return Conflict("User with this email already exists.");

            var user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid().ToString();
            
            _unitOfWork.Users.AddUser(user);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}
