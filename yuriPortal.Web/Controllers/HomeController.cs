using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using yuriPortal.Data.Models;
using yuriPortal.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace yuriPortal.Web.Controllers
{
    public class HomeController : Controller
    {
		ApplicationDbContext db = new ApplicationDbContext();

		public virtual ApplicationUser _UserManager { get; set; }



		public ActionResult GetMenuList()
		{

			try
			{
				//var result = (from m in db.Menus
				//			  select new Models.Menu
				//			  {
				//				  MenuId = m.MenuId,
				//				  ParentMenuId = m.ParentMenuId,
				//				  MenuName = m.MenuName,
				//				  AppPathId = m.AppPathId

				//			  }).ToList();

				//var result = db.Menus.Where(x => x.MenuLevel == 1).ToList();

				//return PartialView("_Menu", db.Menus.ToList());
				//var model = new List<Menu>();

				//using (var context = new ApplicationDbContext())
				//{
				//	model = db.Menus.Where(m => m.MenuLevel == 1).ToList();
				//}

				//var model = db.Menus.AsEnumerable().Where(e => e.ParentMenuId == "MN01").ToList();

				//var model = db.Menus.Include(m => m.Children).Where(m => m.MenuLevel > 0).ToList();
				//return PartialView("_Menu", model.ToList());

				return PartialView("_Menu");
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Content(error);
			}

			//return PartialView("_Menu");
		}

		public static List<Menu> GetChildren(List<Menu> comments, string parentId)
		{
			return comments
					.Where(c => c.ParentMenuId == parentId)
					.Select(c => new Menu
					{
						MenuId = c.MenuId,
						MenuName = c.MenuName,
						ParentMenuId = c.ParentMenuId,
						Children = GetChildren(comments, c.MenuId)
					})
					.ToList();
		}

		[HttpGet]
        public ActionResult Index()
        {
			ViewBag.Message = "Welcome";

			var strUserID = User.Identity.GetUserId();

			if (strUserID == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
				ApplicationUser appUser = userManager.FindByIdAsync(User.Identity.GetUserId()).Result;
				//ApplicationUser appUser = _userManager.FindByIdAsync(strUserID).Result;

				return View(appUser);
			}
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

		[HttpGet]
		public ActionResult Temp()
		{
			ViewBag.Message = "Your temp page.";

			return View();
		}

		[HttpGet]
		public ActionResult TempBox()
		{
			ViewBag.Message = "Your temp page.";

			return View();
		}

		[HttpGet]
		public ActionResult TempLayout()
		{
			ViewBag.Message = "Your temp page.";

			return View();
		}

		public ActionResult error404(){

			ViewBag.Message = "Error 404";

			return View();
		}


		public ActionResult AdminLayout()
		{

			ViewBag.Message = "Admin Layout";

			return View();
		}

	}
}
