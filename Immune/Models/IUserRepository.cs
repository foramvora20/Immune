using System.Collections.Generic;

namespace Immune.Models
{
    public interface IUserRepository
    {
        UserDetails Add(UserDetails userDetails);
        IEnumerable<UserDetails> GetAllUser();

        UserDetails GetUserByEmail(string email);
        UserDetails Login(string email, string password);

    }
}
