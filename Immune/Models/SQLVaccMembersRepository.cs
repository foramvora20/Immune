using System;
using System.Collections;
using System.Collections.Generic;

namespace Immune.Models
{
    public class SQLVaccMembersRepository : IVaccMembersRepository
    {
        private readonly AppDbContext _context;

        public SQLVaccMembersRepository(AppDbContext context)
        {
            _context = context;
        }

        VaccMembers IVaccMembersRepository.Add(VaccMembers vaccMembers)
        {
            _context.vaccMembers.Add(vaccMembers);
            _context.SaveChanges();
            return vaccMembers;
        }

        
        VaccMembers IVaccMembersRepository.GetUserByEmail(string email)
        {
            IEnumerable<VaccMembers> members = _context.vaccMembers;
            foreach (VaccMembers member in members)
            {
                if (member.Email == email)
                {
                    return member;
                }
            }
            return null;
        }

        IEnumerable<VaccMembers> IVaccMembersRepository.GetAllMembers()
        {
            return _context.vaccMembers;
        }

        VaccMembers IVaccMembersRepository.GetVaccMemberById(int Id)
        {
            IEnumerable<VaccMembers> members = _context.vaccMembers;
            foreach(VaccMembers member in members)
            {
                if(member.Id == Id)
                {
                    return member;
                }
            }
            return null;
        }

        void IVaccMembersRepository.Update(VaccMembers vaccMembers)
        {
            var vacMem = _context.vaccMembers.Attach(vaccMembers);
            vacMem.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            
            _context.SaveChanges();
           
            
        }
    }
}
