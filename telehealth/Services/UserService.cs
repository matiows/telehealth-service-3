using telehealth.Context;
using telehealth.Models;
using Microsoft.EntityFrameworkCore;

namespace telehealth.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;

        }

        public async Task<User> CheckUser (int id)
        {
            var user = await _context.Users
                .Where(user => user.UserId == id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                user = new User
                {
                    UserId = id,
                };
            
                _context.Users.Add(user);
            
                await _context.SaveChangesAsync();
            }

            return user;
        }
    }
}
