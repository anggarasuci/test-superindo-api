using ProductApi.Data.Entity;

namespace ProductApi.Data.Service
{
    public interface IUserService
    {
        public Task<UserEntity> LoginUserAsync(string username, string password);
        public Task AddUserAsync(UserEntity user);
    }
}

