using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Concert.Data.DTO;
using Concert.Data.Entity;
using Concert.Data.Repository;




namespace Consert.API.Controllers
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
        public async Task <IActionResult> GetUserById(string id)
        {
            var users = await _unitOfWork.GetUserByIdAsync(id);
            return Ok(_mapper.Map<UserDto>(users));
        }
        [HttpGet("getUserIdFromEmail")]
        public async Task <IActionResult> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return Ok(_mapper.Map<UserDto>(user));
        }
        [HttpPost("addUser")]
        public async Task <IActionResult> AddUser([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddUserAsync(user);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, [FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _userRepository.UpdateUserAsync(user);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteUser(string id)
        {   var user = await _userRepository.GetUserByIdAsync(id);
            await _userRepository.DeleteUserAsync(user);
            return Ok();
        }
    }
}