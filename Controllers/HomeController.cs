using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using Online_Recharge.Db;
using Online_Recharge.Models;
using System;
using System.Collections.Immutable;
using System.Data.Common;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Online_Recharge.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext MyDbContext;
        private readonly ILogger<HomeController> _logger;
		public MultipleModelClass myModel = new MultipleModelClass();
        public HomeController(ILogger<HomeController> logger , MyDbContext context)
        {
			_logger = logger;
			MyDbContext = context;
			myModel.dataPackages = MyDbContext.dataPackages.ToList();
			myModel.internetOrders = MyDbContext.internetOrders.ToList();

        }
		public IActionResult dataServices()
		{
			var pckg = MyDbContext.dataPackages.Where(i => i.type == "Internet package").ToList();
			var special = MyDbContext.dataPackages.Where(i => i.type == "Special recharge").ToList();
			var topup = MyDbContext.dataPackages.Where(i => i.type == "Top up").ToList();
			ViewBag.pckg = pckg.ToList();
			ViewBag.special = special.ToList();
			ViewBag.topup = topup.ToList();
			return View();
		}
		public IActionResult site()
		{
			return View();
		}
		public IActionResult UserProfile()
		{
			return View();
		}
        public IActionResult Index()
        {  
			return View();
        }

		[HttpPost]
		public IActionResult searchusr()
		{

			var email = Request.Form["signin_mail"].ToString();
			var pass = Request.Form["signin_pass"].ToString();

			List<Login_info> signin = MyDbContext.Login_info.Where(u => u.email == email && u.pass == pass).ToList();

			if (signin.Count > 0 )
			{
				HttpContext.Session.SetString("name" , signin[0].name.ToString());
				HttpContext.Session.SetString("email" , signin[0].email.ToString());
				HttpContext.Session.SetString("num" , signin[0].number.ToString());
				HttpContext.Session.SetString("Uid" , signin[0].id.ToString());
				HttpContext.Session.SetString("status" , signin[0].status.ToString());
				SessionClass.Name = signin[0].name.ToString();
				SessionClass.Email = signin[0].email.ToString();
				SessionClass.Number = signin[0].number.ToString();
				SessionClass.Uid = signin[0].id.ToString();
				SessionClass.status = signin[0].status.ToString();
				if (SessionClass.status == "admin")
				{
					return Content("admin");
				}
				else
				{
				return Content("success");

				}
			}
			else
			{
				return Content("fail");
			}

		}

		public IActionResult logout()
		{
			HttpContext.Session.Clear();
			SessionClass.Name = "";
			SessionClass.Email = "";
			SessionClass.Uid = "";
			SessionClass.status = "Guest";
			return RedirectToAction("Index", "Home");

		}
		[HttpPost]
		public IActionResult Updateusr()
		{
			try
			{
				var name = Request.Form["name"];
				var no = Request.Form["no"];
				var email = Request.Form["mail"];

				var userIds = SessionClass.Uid;

				if (string.IsNullOrEmpty(userIds))
				{
					return BadRequest("Invalid session");
				}

				var user = MyDbContext.Login_info.FirstOrDefault(p => p.id.ToString() == userIds);

				if (user == null)
				{
					return NotFound("User not found");
				}

				user.name = name;
				user.email = email;
				user.number = no;

				MyDbContext.SaveChanges();
				HttpContext.Session.Clear();
				SessionClass.Name = "";
				SessionClass.Email = "";
				SessionClass.Uid = "";
				var signin = MyDbContext.Login_info.Where(u => u.id.ToString() == userIds).ToList();
				HttpContext.Session.SetString("name", signin[0].name.ToString());
				HttpContext.Session.SetString("email", signin[0].email.ToString());
				HttpContext.Session.SetString("num", signin[0].number.ToString());
				HttpContext.Session.SetString("Uid", signin[0].id.ToString());
				SessionClass.Name = signin[0].name.ToString();
				SessionClass.Email = signin[0].email.ToString();
				SessionClass.Number = signin[0].number.ToString();
				SessionClass.Uid = signin[0].id.ToString();
				return Content("success");

			}
			catch (Exception ex)
			{
				return StatusCode(500, $"An error occurred: {ex.Message}");
			}
		}

		public IActionResult updatepass()
		{
			try { 
				var oldpass = Request.Form["oldpass"];
				var newpass = Request.Form["newpass"];
				var userIds = SessionClass.Uid;
				var user = MyDbContext.Login_info.FirstOrDefault(p => p.id.ToString() == userIds);

				if (user == null)
				{
					return NotFound("User not found");
				}
				user.pass = newpass;

				MyDbContext.SaveChanges();
				var signin = MyDbContext.Login_info.Where(u => u.id.ToString() == userIds).ToList();
				SessionClass.Password = "";
				HttpContext.Session.SetString("pass", signin[0].pass.ToString());
				SessionClass.Password = signin[0].pass.ToString();

				return Content("success");

			}
			catch (Exception ex)
			{
				return StatusCode(500, $"An error occurred: {ex.Message}");
			}
		}

		public IActionResult AdminPanel()
		{
			return View();
		}

		[HttpPost]
        public IActionResult createusr()
        {

			var getname = Request.Form["name"].ToString();
			var getnum = Request.Form["num"].ToString();
			var getemail = Request.Form["email"].ToString();
			var getpass = Request.Form["pass"].ToString();

			List<Login_info> login = MyDbContext.Login_info.Where(u => u.email == getemail).ToList();

			if (login.Count > 0)
			{
				return Content("already");
			}
			else
			{
				Login_info login_Info = new Login_info()
				{
					email = getemail,
					name = getname,
					number = getnum,
					pass = getpass,
				};

				MyDbContext.Login_info.Add(login_Info);
				MyDbContext.SaveChanges();
				return Content("success");

			}
		}

		[HttpPost]
		public IActionResult easyload()
		{
			var num = Request.Form["mobileNumber"].ToString();
			var price = Request.Form["payment"].ToString();
			var city = Request.Form["city"].ToString();
			var operators = Request.Form["operator"].ToString();
			var status = "Guest";
			if(SessionClass.Uid == "")
			{

                easyloadData.number = num;
                easyloadData.price = price;
                easyloadData.city = city;
                easyloadData.operators = operators;
                easyloadData.status = status;

            return Content("success");
			}
			else
			{
				return Content("fail");
			}				
        }

		[HttpPost]
		public IActionResult EasyLoadDone()
		{

            var num = Request.Form["number"].ToString();
            var price = Request.Form["price"].ToString();
            var city = Request.Form["city"].ToString();
            var operators = Request.Form["operators"].ToString();
            var cname = Request.Form["cname"].ToString();
            var ccnum = Request.Form["ccnum"].ToString();
			var status = "Guest";

			if(SessionClass.Uid == "")
			{
                easyLoad easyLoadusr = new easyLoad()
                {
                    number = num,
                    price = price,
                    city = city,
                    operators = operators,
                    cname = cname,
                    ccnum = ccnum,
                    status = status
                };
                MyDbContext.easyLoad.Add(easyLoadusr);
                MyDbContext.SaveChanges();
                return Content("success");
            }
			else if(SessionClass.Uid != "")
			{
                easyLoad easyLoadusr = new easyLoad()
                {
                    number = num,
                    price = price,
                    city = city,
                    operators = operators,
                    cname = cname,
                    ccnum = ccnum,
                    status = "User"
                };
                MyDbContext.easyLoad.Add(easyLoadusr);
                MyDbContext.SaveChanges();
                return Content("success");
			}
			else
			{
                return Content("fail");
				
			}
			
		}
        public IActionResult Privacy()
        {
            return View();
        }

		[HttpPost]
		public IActionResult servicesProvide()
		{
			var payment = Request.Form["payment"].ToString();
			var operators = Request.Form["operator"].ToString();
			var validity = Request.Form["validity"].ToString();
			var desc = Request.Form["desc"].ToString();
			var type = Request.Form["type"].ToString();

			dataPackages datapkg = new dataPackages
			{
				payment = payment,
				operators = operators,
				validity = validity,
				desc = desc,
				type = type

			};
			MyDbContext.dataPackages.Add(datapkg);
			MyDbContext.SaveChanges();
			return Content("success");

		}

		public IActionResult services()
		{
			return View(myModel);
		}
		public IActionResult editDatapackages(string myid)
		{
			List<dataPackages> edit = MyDbContext.dataPackages.Where(u => u.id.ToString() == myid).ToList();
			if (edit != null)
			{
				editClass.payment = edit[0].payment.ToString();
				editClass.operators = edit[0].operators.ToString();
				editClass.desc = edit[0].desc.ToString();
				editClass.validity = edit[0].validity.ToString();
				editClass.id = edit[0].id.ToString();
				editClass.type = edit[0].type.ToString();
				return View();
			}
			else
			{
				return Content("false");
			}				
		}

		public IActionResult updatePackage()
		{
			var payment = Request.Form["payment"].ToString();
			var operators = Request.Form["operator"].ToString();
			var validity = Request.Form["validity"].ToString();
			var desc = Request.Form["desc"].ToString();
			var package = Request.Form["package"].ToString();
			var id = editClass.id.ToString();

			var update = MyDbContext.dataPackages.FirstOrDefault(i=> i.id.ToString()==id);
			if (update != null)
			{
				update.payment = payment;
				update.operators = operators;
				update.desc = desc;
				update.validity = validity;
				update.type = package;
				MyDbContext.SaveChanges();
				return Content("success");
			}
			else
			{
				return Content("false");
			}


		}

		public IActionResult deleteDatapackages(string myid)
		{
            var edit = MyDbContext.dataPackages.FirstOrDefault(u => u.id.ToString() == myid);

			if(edit != null)
			{
				MyDbContext.dataPackages.Remove(edit);
				MyDbContext.SaveChanges();
			}
			else
			{
			return Content("Data package not deleted");
			}
			return Content("Deleted Successfully");
			
        }

		public IActionResult UsersInfo()
		{
			var usrs = MyDbContext.Login_info.Where(s=>s.status == "User").ToList();
			var feedback = MyDbContext.feedback.ToList();
			var easyLoad = MyDbContext.easyLoad.ToList();

			ViewBag.usrs = usrs;
			ViewBag.feedback = feedback;
			ViewBag.easyLoad = easyLoad;
			return View(myModel);
		}
		public IActionResult CustCare()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
		public IActionResult Payment(string price )
		{
			ViewBag.price = price;
			return View();
		}
		[HttpPost]
		public IActionResult UsersOrders() {

            var num = Request.Form["number"].ToString();
            var name = Request.Form["name"].ToString();
            var mail = Request.Form["mail"].ToString();
            var cname = Request.Form["cname"].ToString();
            var ccnum = Request.Form["ccnum"].ToString();
            var id = Request.Form["id"];
			
			 usersOrders internetusr = new usersOrders()
                {
                    number = num,
                    cname = cname,
                    ccnum = ccnum,
                    mail = mail,
					fname = name,
					datapackage_id = int.Parse(id)
             };
                MyDbContext.internetOrders.Add(internetusr);
                MyDbContext.SaveChanges();
                return Content("success");
            
		}
		public IActionResult edit(string id, string payment, string desc, string operators, string validation)
		{

			var edit = MyDbContext.dataPackages.FirstOrDefault(u => u.id.ToString() == id);

			if (edit == null)
			{
				return NotFound("User not found");
			}
			else
			{
				edit.payment = payment;
				edit.operators = operators;
				edit.validity = validation;
				edit.desc = desc;
				MyDbContext.SaveChanges();
                return RedirectToAction("services");
			}


		}

		[HttpPost]
		public IActionResult contactus()
		{
            var name = Request.Form["name"].ToString();
            var email = Request.Form["email"].ToString();
            var message = Request.Form["message"].ToString();
            var inquiry = Request.Form["inquiry"].ToString();
            var phone = Request.Form["phone"].ToString();


            contact contact = new contact()
            {
                name = name,
                email = email,
                message = message,
				inquiry = inquiry,
				phone = phone
            };

            MyDbContext.contact.Add(contact);
            MyDbContext.SaveChanges();
            return Content("success");
        }

        [HttpPost]
		public IActionResult feedbackform()
		{
            var name = Request.Form["name"].ToString();
            var email = Request.Form["email"].ToString();
            var message = Request.Form["message"].ToString();


			feedback feedbackfrm = new feedback()
			{
				name = name,
				email = email,
				msg = message
			};

			MyDbContext.feedback.Add(feedbackfrm);
			MyDbContext.SaveChanges();
			return Content("success");
        }

        public IActionResult InternetPayment( string id , string price)
		{
            ViewBag.price = price;
            ViewBag.id = id;
            return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}