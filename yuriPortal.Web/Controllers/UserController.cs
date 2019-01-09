using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yuriPortal.Core;
using yuriPortal.Data.Models;
using yuriPortal.Web.Helper;
using yuriPortal.Data.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Data;
using yuriPortal.Core.Interfaces;
using System.Configuration;
using yuriPortal.Helper;
using Microsoft.AspNet.Identity.EntityFramework;
using yuriPortal.Core.Repository;

namespace yuriPortal.Web.Controllers
{
    public class UserController : Controller
    {

		UserProfileRepository dbUser = new UserProfileRepository();

		public UserController()
		{
		}


		public UserController(ApplicationUserManager userManager)
		{
			UserManager = userManager;
		}

		private ApplicationUserManager _userManager;
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		// GET: User
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult GetUsers(int pageNumber = 1, int pageSize = 15, string searchText = null)
		{
			try
			{
				var pagedData = Pagination.PagedResult(dbUser.GetUsers(searchText), pageNumber, pageSize);
				return Json(pagedData, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}


		public ActionResult UserCreate()
		{
			return PartialView("UserCreate");
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> SaveUserInfo(UserSaveViewModel userInfo)
		{
			try
			{
				if (ModelState.IsValid)
				{
					const string iniTialPassword = "User@123456";

					var user = new ApplicationUser { Email = userInfo.Email, UserName = userInfo.Email, PhoneNumber = userInfo.PhoneNumber };
					var result = await _userManager.CreateAsync(user, iniTialPassword);
					if (result.Succeeded)
					{
						var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
						var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
						await _userManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
					}

					dbUser.SaveUserProfile(user.Id, userInfo);

					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}

				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, SaveError = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
			}

		}


		[HttpPost]
		[AllowAnonymous]
		public ActionResult SaveUserPhoto(string UserName)
		{
			FileManager fm = new FileManager();
			HttpPostedFileBase file = Request.Files[0];
			string strSaveFileName = "";
			string userpicPath = ConfigurationManager.AppSettings["userpicPath"];

			try
			{
				if (file.ContentLength > 0)
				{
					var fileName = Path.GetFileName(file.FileName);
					strSaveFileName = fm.SaveFile(file, userpicPath);

					var guid = Guid.NewGuid();

					dbUser.SaveUserPhoto(UserName, fileName, Path.GetExtension(fileName), strSaveFileName);
				}

				return Json(new { success = true, fileName = strSaveFileName }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, ErrorMsg = ex.Message }, JsonRequestBehavior.AllowGet);
			}
			finally
			{
				if (fm != null)
					fm = null;
			}
			
		}


		public ActionResult UserDetail(string userNm)
		{
			if (userNm == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var defaultImagePath = ConfigurationManager.AppSettings["defaultImagePath"];

			return PartialView("UserDetail", dbUser.GetUserDetail(userNm, defaultImagePath));
		}

	}
}