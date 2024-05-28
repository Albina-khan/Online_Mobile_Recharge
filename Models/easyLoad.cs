using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Online_Recharge.Models
{
	public class easyLoad
	{
		[Key]
		[BindProperty]
		public int Id { get; set; }

		[Required]
		[BindProperty]
		public string number { get; set; }
		[Required]
		[BindProperty]
		public string operators { get; set; }
		[Required]
		[BindProperty]
		public string city { get; set; }
		[Required]
		[BindProperty]
		public string status { get; set; }
		[Required]
		[BindProperty]
		public string price { get; set; }

        [Required]
        [BindProperty]
        public string cname { get; set; }

        [Required]
        [BindProperty]
        public string ccnum { get; set; }

    }
}
