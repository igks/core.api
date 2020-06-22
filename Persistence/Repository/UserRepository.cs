using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.API.Core.IRepository;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;
using Microsoft.EntityFrameworkCore;

namespace CORE.API.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    user = null; // set user to null if password not verify.

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await context.User.ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            return await context.User.FindAsync(id);
        }

        public async Task<PagedList<User>> GetPaged(UserParams userParams)
        {
            var users = context.User.AsQueryable();

            if (!string.IsNullOrEmpty(userParams.Firstname))
            {
                users = users.Where(u => u.Firstname.Contains(userParams.Firstname));
            }

            if (userParams.isDescending)
            {
                if (!string.IsNullOrEmpty(userParams.OrderBy))
                {
                    switch (userParams.OrderBy.ToLower())
                    {
                        case "firstname":
                            users = users.OrderByDescending(u => u.Firstname);
                            break;
                        default:
                            users = users.OrderByDescending(u => u.Id);
                            break;
                    }
                }
                else
                {
                    users = users.OrderByDescending(u => u.Id);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(userParams.OrderBy))
                {
                    switch (userParams.OrderBy.ToLower())
                    {
                        case "firstname":
                            users = users.OrderBy(u => u.Firstname);
                            break;
                        default:
                            users = users.OrderBy(u => u.Id);
                            break;
                    }
                }
                else
                {
                    users = users.OrderBy(u => u.Id);
                }
            }

            return await PagedList<User>
                .CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public void Add(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.User.Add(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public void Update(User user)
        {
            context.User.Attach(user);
            this.context.Entry(user).State = EntityState.Modified;
        }

        public void Remove(User user)
        {
            context.Remove(user);
        }
    }
}