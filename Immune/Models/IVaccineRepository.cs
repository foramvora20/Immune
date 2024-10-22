using System.Collections.Generic;

namespace Immune.Models
{
    public interface IVaccineRepository
    {
        Vaccine Add(Vaccine vaccine);

        IEnumerable<Vaccine> GetAllVaccineDest();

        Vaccine GetVaccDestCity(string city);

        Vaccine GetRow(string city, string date);

        void Update(Vaccine vaccine);

        List<string> City();
    }
}
