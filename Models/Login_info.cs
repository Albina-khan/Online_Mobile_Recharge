using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Online_Recharge.Models
{
    public class Login_info
    {

        [Key]
        [BindProperty]
        public int id { get; set; }


        [Required]
        [BindProperty]
        public string name { get; set; }


        [Required]
        [BindProperty]
        public string number { get; set; }


        [Required]
        [BindProperty]
        public string email { get; set; }


        [Required]
        [BindProperty]
        public string pass { get; set; }

        [Required]
        [BindProperty]
        public string status { get; set; }
    }
}
