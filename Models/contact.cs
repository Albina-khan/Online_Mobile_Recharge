using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Online_Recharge.Models
{
    public class contact
    {
        [Key]
        [BindProperty]
        public int id { get; set; }


        [Required]
        [BindProperty]
        public string name { get; set; }


        [Required]
        [BindProperty]
        public string email { get; set; }


        [Required]
        [BindProperty]
        public string phone { get; set; }

        [Required]
        [BindProperty]
        public string inquiry { get; set; }


        [Required]
        [BindProperty]
        public string message { get; set; }

    }
}
