using telehealth.Models;

namespace telehealth.Services
{
    public interface IUserService
    {
        public Task<User> CheckUser(int id);

    }
}
