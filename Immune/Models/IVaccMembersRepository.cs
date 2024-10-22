using System.Collections;
using System.Collections.Generic;

namespace Immune.Models
{
    public interface IVaccMembersRepository
    {
        VaccMembers Add(VaccMembers vaccMembers);
        
        VaccMembers GetVaccMemberById(int Id);
        IEnumerable<VaccMembers> GetAllMembers();
        void Update(VaccMembers vaccMembers);
        VaccMembers GetUserByEmail(string email);
    }
}
