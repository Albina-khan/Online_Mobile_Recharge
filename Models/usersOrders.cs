using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Online_Recharge.Models
{
    public class usersOrders
    {
        [Key]
        [BindProperty]
        public int id { get; set; }

        [Required]
        [BindProperty]
        public string fname { get; set; }
        [Required]
        [BindProperty]
        public string mail { get; set; }
        [Required]
        [BindProperty]
        public string number { get; set; }
        [Required]
        [BindProperty]
        public string cname { get; set; }
        [Required]
        [BindProperty]

        public string ccnum { get; set; }

        [Required]
        [BindProperty]
        public int datapackage_id { get; set; }

    }
}
