using System.Collections.Generic;

namespace Immune.Models
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public SQLUserRepository(AppDbContext context)
        {
            this._context = context;
        }


        UserDetails IUserRepository.Add(UserDetails userDetails)
        {
              _context.Users.Add(userDetails);
               _context.SaveChanges();
            return userDetails;
        }

        IEnumerable<UserDetails> IUserRepository.GetAllUser()
        {
            return _context.Users;
        }
        UserDetails IUserRepository.GetUserByEmail(string  email)
        {
            IEnumerable<UserDetails> users = _context.Users;
            foreach(UserDetails user in users)
            {
                if(user.UserEmail == email)
                {
                    return user;
                }
            }
            return null;
        }

        UserDetails IUserRepository.Login(string email, string password)
        {
            IEnumerable<UserDetails> users = _context.Users;
            foreach (UserDetails user in users)
            {
                if (user.UserEmail == email && user.password == password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
