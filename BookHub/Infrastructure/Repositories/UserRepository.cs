using Core.Models;
using Infrastructure.Contexts;
using Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<BookUser>, IUserRepository
    {
        public UserRepository(BookContext context) : base(context)
        {
        }

        public async Task<BookUser> SelectUserWithLoginInfoAsync(string username, string password)
        {
            return await Context.BookUser
                .FirstOrDefaultAsync(u => u.Username.Equals(username) &&
                                     u.Password.Equals(password));
        }

        public async Task<bool> CheckAlreadyRegisteredUserAsync(SignupUserDTO signupUserDTO)
        {
            return await Context.BookUser
                    .FirstOrDefaultAsync(
                        u => u.Username.Equals(signupUserDTO.Username) ||
                             u.Email.Equals(signupUserDTO.Email)
                    ) != null;
        }

        public async Task<BookUser> SelectUserInformationByIdAsync(int id)
        {
            return await Context.BookUser
                .Include(u => u.BookPosts)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<BookUser> SelectUserInformationByUsernameAsync(string username)
        {
            return await Context.BookUser
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> CheckEmailAlreadyInUse(BookUser bookUser)
        {
            return await Context.BookUser
                .FirstOrDefaultAsync(u => u.Email.Equals(bookUser.Email) && u.Id != bookUser.Id)
                != null;
        }

        public void DetachLocal(BookUser bookUser)
        {
            // 
            var local = Context.BookUser
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(bookUser.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                Context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            Context.Entry(bookUser).State = EntityState.Modified;

            // save 
            Context.SaveChanges();
        }
    }
}
