using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Xml.Linq;
using Immune.Models;
using Microsoft.AspNetCore.Components.Forms;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Immune.Controllers
{
    public class VaccMembersController : Controller
    {
        public readonly IVaccMembersRepository _membersRepository;
        public readonly IUserRepository _userRepository;
        public readonly IVaccineRepository _vaccineRepository;


        public VaccMembersController(IVaccMembersRepository membersRepository, IVaccineRepository vaccineRepository, IUserRepository userRepository)
        {
            _membersRepository = membersRepository;
            _vaccineRepository = vaccineRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<VaccMembers> vacc = _membersRepository.GetAllMembers();

            return View(vacc);
        }

        public IActionResult AddMembers()
        {
            List<string> city = new List<string>();
            city=_vaccineRepository.City();
            List<SelectListItem> ObjList = new List<SelectListItem>();
            foreach(string cityItem in city)
            {
                SelectListItem Obj = new SelectListItem();
                Obj.Text = cityItem;
                Obj.Value = cityItem;
                ObjList.Add(Obj);
            }
            ViewBag.city = ObjList;

            IEnumerable<Vaccine> vacc = _vaccineRepository.GetAllVaccineDest();
            ViewBag.vacc = vacc;
            return View();
        }
        [HttpPost]
        public IActionResult AddMembers(VaccMembers model)
        {
            if (ModelState.IsValid)
            {
                UserDetails user = _userRepository.GetUserByEmail(model.Email);
                VaccMembers member = _membersRepository.GetUserByEmail(model.Email);
                if (user == null && member==null)
                {
                    VaccMembers vaccMembers = new VaccMembers
                    {
                        Name = model.Name,
                        DOB = model.DOB,
                        Email = model.Email,
                        City = model.City,
                        uId = model.uId,
                    };
                    _membersRepository.Add(vaccMembers);
                    IEnumerable<VaccMembers> vacc = _membersRepository.GetAllMembers();
                    return View("Index",vacc);
                 }
                else
                {
                    ViewBag.msg = "Email already exists";
                    IEnumerable<VaccMembers> vacc1 = _membersRepository.GetAllMembers();
                    return View();
                }
            }

            return View();
        }

        public IActionResult BookSlot(int Id)
        {
            IEnumerable<VaccMembers> members = _membersRepository.GetAllMembers();
            string city = null;
            foreach(VaccMembers member in members)
            {
                if(member.Id == Id)
                {
                    city = member.City;
                }
            }
            List<string> dates = new List<string>();
            IEnumerable<Vaccine> vacc = _vaccineRepository.GetAllVaccineDest();
            List<SelectListItem> ObjList = new List<SelectListItem>();
            foreach (Vaccine vaccine1 in vacc)
            {
                if(vaccine1.city == city)
                {
                    SelectListItem Obj = new SelectListItem();
                    Obj.Text = vaccine1.Date;
                    Obj.Value = vaccine1.Date;
                    ObjList.Add(Obj);
                }
            }
            TempData["city"] = city;
            TempData["uId"] = Id;
            ViewBag.dates = ObjList;
            return View();
        }

        [HttpGet]
        public IActionResult slot(string Date, string dose)
        {
            bool flag=false;
            IEnumerable<Vaccine> vacc = _vaccineRepository.GetAllVaccineDest();
            VaccMembers vaccMembers = null;
            Vaccine vaccine = null;
            foreach (Vaccine vaccine1 in vacc)
            {
                if ((string)TempData["city"]==vaccine1.city && vaccine1.Date == Date)
                {
                    int id = (int)TempData["uId"];
                        
                     vaccMembers = _membersRepository.GetVaccMemberById(id);
                     vaccine = _vaccineRepository.GetRow((string)TempData["city"], Date);
                    int age = 0;
                    age = DateTime.Now.Subtract(Convert.ToDateTime(vaccMembers.DOB)).Days;
                    age = age / 365;
                    if (dose == "dose1")
                    {
                        
                      //  if(int.Parse(vaccine1.Dose1) != 0)
                       // {
                            if (age >= 18)
                            {
                               // int temp = (int.Parse(vaccine1.Dose1) - 1);
                                vaccine.Dose1 = Date;
                                vaccMembers.Dose1 = Date;
                                ViewBag.msg = "Your slot has booked successfully !!";
                                
                            }
                            else
                            {
                                ViewBag.msg = "You can not take this vaccine you are not 18";
                            }
                        //}
                    }
                    else if(dose == "dose2")
                    {
                     //  if (int.Parse(vaccine1.Dose2) != 0)
                       // {
                            if(vaccMembers.Dose1 != null)
                            {
                                DateTime dose1 = Convert.ToDateTime(vaccMembers.Dose1);
                                int diff = dose1.Subtract(Convert.ToDateTime(Date)).Days;
                                if(diff < -60)
                                {
                                //int temp = (int.Parse(vaccine1.Dose2) - 1);
                                vaccine.Dose2 = Date;
                                    vaccMembers.Dose2 = Date;
                                    ViewBag.msg = "Your slot has booked successfully !!";
                                   
                                }
                                else
                                {
                                    ViewBag.msg = "You have not completed 3 months afetr taking the first dose !!!";
                                    
                                }
                            }
                            else
                            {
                                ViewBag.msg = "You have not taken Dose1.So, please take Dose1.";
                               
                            }
                      //  }
                        //else
                        //{
                          //  ViewBag.msg = "Not enough vaccine. Check for other dates";
                            
                        //}
                    }
                    else
                    {
                        ViewBag.msg = "Please type dose1 or dose2 only. It is case sensitive.";
                        
                    }
                    flag = true;
                    break;
                }
            }
            if(flag)
            {

                _membersRepository.Update(vaccMembers);
                _vaccineRepository.Update(vaccine);
                IEnumerable<VaccMembers> vacc1 = _membersRepository.GetAllMembers();
                return View("Index", vacc1);
               
            }
            return View();
        }

        public IActionResult Edit(int Id)
        {
            VaccMembers vacc = _membersRepository.GetVaccMemberById(Id);
            return View(vacc);
        }

        [HttpPost]
        public IActionResult Edit(VaccMembers model)
        {
            if (ModelState.IsValid)
            {
                _membersRepository.Update(model);
                ViewBag.msg = "Details Updated Successfully.";
                IEnumerable<VaccMembers> vacc1 = _membersRepository.GetAllMembers();
                return View("Index", vacc1);
            }
            return View();
        }

        public IActionResult Details(int Id)
        {
            VaccMembers vacc = _membersRepository.GetVaccMemberById(Id);
            return View(vacc);
        }

        public IActionResult ShowCert(int Id)
        {
            var vacc = _membersRepository.GetVaccMemberById(Id);
            if(vacc.Dose1 != null && vacc.Dose2 != null)
            {
                return View(vacc);
            }
            else
            {
                ViewBag.msg = "You have not taken both the doses. So, You can not generate certificate.";
                IEnumerable<VaccMembers> vacc1 = _membersRepository.GetAllMembers();
                return View("Index", vacc1);
            }
        }
        public JsonResult GetDose(string city)
        {
            IEnumerable<Vaccine> vacc= _vaccineRepository.GetAllVaccineDest();
            string dose1 = null, dose2 = null;
            foreach(var item in vacc)
            {
                if (item.city == city)
                {
                    dose1 = item.Dose1;
                    dose2 = item.Dose2;
                }
            }
            List<string> dose = new List<string>();
            dose.Add(dose1);
            dose.Add(dose2);
            return Json(dose);

        }

        
    }
}
