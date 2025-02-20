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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }


        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }


        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.AddUser(user);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userDto)
        {
            var existingUser = await _unitOfWork.Users.GetUserByIdAsync(id);
            if (existingUser == null) return NotFound();

            var updatedUser = _mapper.Map(userDto, existingUser);
            _unitOfWork.Users.UpdateUser(updatedUser);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            _unitOfWork.Users.DeleteUser(user);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
