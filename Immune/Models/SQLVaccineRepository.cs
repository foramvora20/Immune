using System.Collections.Generic;

namespace Immune.Models
{
    public class SQLVaccineRepository : IVaccineRepository
    {
        private readonly AppDbContext _context;

        public SQLVaccineRepository(AppDbContext context)
        {
            _context = context;
        }

        Vaccine IVaccineRepository.Add(Vaccine vaccine)
        {
            _context.Vaccines.Add(vaccine);
            _context.SaveChanges();
            return vaccine;
        }

        IEnumerable<Vaccine> IVaccineRepository.GetAllVaccineDest()
        {
            return _context.Vaccines;
        }
        Vaccine IVaccineRepository.GetVaccDestCity(string city)
        {
            IEnumerable<Vaccine> vaccines = _context.Vaccines;
            foreach (Vaccine vac in vaccines)
            {
                if (vac.city == city)
                {
                    return vac;
                }
            }
            return null;
        }

        List<string> IVaccineRepository.City()
        {
            IEnumerable<Vaccine> temp = _context.Vaccines;
            List<string> city = new List<string>();
            foreach (Vaccine vac in temp)
            {
                if (!(city.Contains(vac.city)))
                {

                    city.Add(vac.city);
                }

            }
            return city;
        }

        Vaccine IVaccineRepository.GetRow(string city, string date)
        {
            IEnumerable<Vaccine> vacc = _context.Vaccines;
            foreach(Vaccine vac in vacc)
            { 
                if(vac.city == city && vac.Date == date)
                {
                    return vac;
                }
            }
            return null;
        }

        void IVaccineRepository.Update(Vaccine vaccine)
        {
            var vac = _context.Vaccines.Attach(vaccine);
            vac.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        
    }
}
