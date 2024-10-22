using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Immune.Models;
//using System.Web.Security;

namespace Immune.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserDetails model)
        {
            UserDetails success = _userRepository.Login(model.UserEmail, model.password);
            if (model.UserEmail == "admin@gmail.com" && model.password == "123")
            {
                return RedirectToAction(actionName: "Index", controllerName: "Vaccine");
            }
            if (success != null)
            {
                HttpContext.Session.SetString("UserEmail", model.UserEmail);
                HttpContext.Session.SetInt32("UserId", success.Id);
                return RedirectToAction(actionName: "Index", controllerName: "VaccMembers");
                //return View("SignUp");
            }
            else
            {
                return View("");
            }


        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","USer");
        }
            

        [HttpPost]
        public IActionResult SignUp(UserDetails model)
        {
               if (ModelState.IsValid)
               {  
                    UserDetails user = _userRepository.GetUserByEmail(model.UserEmail);
                    if(user == null)
                    {
                        if(model.cpassword == model.password)
                        {
                            UserDetails userDetails = new UserDetails
                            {
                                UserName = model.UserName,
                                UserEmail = model.UserEmail,
                                password = model.password,
                                cpassword = model.cpassword
                            };
                            Console.WriteLine(userDetails);
                            _userRepository.Add(userDetails);
                            return View("login");
                        }
                        else
                        {
                            ViewBag.error1 = "Password and Confirm password must match";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.error = "Email already exists";
                        return View();
                    }
                    
               }
            Console.WriteLine("Hello");
            return View();
            
        }

        public IActionResult SignOut()
        {
            return View();
        }
    }
}
