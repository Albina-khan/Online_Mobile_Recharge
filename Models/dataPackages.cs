using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Online_Recharge.Models
{
	public class dataPackages
	{
		[Key]
		[BindProperty]
		public int id { get; set; }


		[Required]
		[BindProperty]
		public string payment { get; set; }


		[Required]
		[BindProperty]
		public string desc { get; set; }


		[Required]
		[BindProperty]
		public string operators { get; set; }


		[Required]
		[BindProperty]
		public string validity { get; set; }
		[Required]
		[BindProperty]
		public string type { get; set; }
	}

}
