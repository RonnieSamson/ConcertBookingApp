using AutoMapper;
using Concert.Data.DTO;
using Concert.MAUI.Models;

namespace Concert.MAUI.Services
{
    public class UserService : IUserService
    {
        private readonly IRestService _restService;
        private readonly IMapper _mapper;

        public UserService(IRestService restService, IMapper mapper)
        {
            _restService = restService;
            _mapper = mapper;
        }


        public async Task<List<User>?> GetAllUsersAsync()
        {
            return await _restService.GetAsync<List<User>>("users");
        }


        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _restService.GetAsync<User>($"user/{id}");
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var userDto = await _restService.GetAsync<UserDto>($"user/getUserByEmail?email={email}");
            if (userDto == null) return null;

            return _mapper.Map<User>(userDto);
        }


        public async Task SaveUserAsync(User user, bool isNewUser)
        {
            var userDto = _mapper.Map<UserDto>(user);

            if (isNewUser)
                await _restService.PostAsync<User>("users", userDto);
            else
                await _restService.PutAsync<User>($"users/{user.Id}", userDto);
        }


        public async Task DeleteUserAsync(string id)
        {
            await _restService.DeleteAsync($"users/{id}");
        }
    }
}

