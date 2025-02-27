﻿using AutoMapper;
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
            return await _restService.GetAsync<User>($"users/{id}");
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _restService.GetAsync<User>($"User/getUserByEmail?email={email}"); ;
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

