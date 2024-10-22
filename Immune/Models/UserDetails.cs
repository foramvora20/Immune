using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Immune.Models
{
    public class UserDetails
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Username..")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Please Enter Email..")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please Enter Password..")]
        public string password { get; set; }

        [Required(ErrorMessage = "Please Again Enter Password..")]
        public string cpassword { get; set; }



    }
}
