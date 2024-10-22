using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Immune.Models;

namespace Immune.Controllers
{
    public class VaccineController : Controller
    {
        private readonly IVaccineRepository _vaccRepository;

        public VaccineController(IVaccineRepository vaccRepository)
        {
            _vaccRepository = vaccRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Vaccine> vacc = _vaccRepository.GetAllVaccineDest();

            return View(vacc);
        }

        public IActionResult AddCenter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCenter(Vaccine model)
        {
            if (ModelState.IsValid)
            {

                
                    Vaccine vacc = new Vaccine
                    {
                        city = model.city,
                        Dose1 = model.Dose1,
                        Dose2 = model.Dose2,
                        Date = model.Date,
                    };
                    _vaccRepository.Add(vacc);
                    IEnumerable<Vaccine> vac = _vaccRepository.GetAllVaccineDest();
                    return RedirectToAction("Index", vac);

                
                
            }
            return View();
        }

        
    }
}
