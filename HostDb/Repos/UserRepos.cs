using HostDb.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace HostDb.Repos
{
    public class UserRepos
    {
        private TestContext _context;

        public UserRepos(TestContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user)
        {
            var exists = await _context.Users.AnyAsync(e => e.UserName.Equals(user.UserName));

            if (exists)
                return null;
            
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new();
        }

       public async Task<User>Login(string username)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            
              
            
        }
        public async Task<User>PasswordSignInAsync(string password)
        {
            return await _context.Users.FirstOrDefaultAsync(s => s.Password == password);
        }

        public async Task<User> Update( User user)
        {
            var result  = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            if(result == null)
            {
                return null;
            }
            result.Name = user.Name;
            result.UserName = user.UserName;
            _context.Users.Update(result);
            await _context.SaveChangesAsync();
            return result;
        }
        public async Task<User>DeleteUser(int id)
        {
            var result=  await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (result == null)
                return null;
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }


        
        
    }
}
