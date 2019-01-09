using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yuriPortal.Core;
using yuriPortal.Web.Helper;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using yuriPortal.Core.Repository;
using yuriPortal.Helper;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;

namespace yuriPortal.Web.Controllers
{
    public class CommCodeController : Controller
    {
		ICodeGroupRepository dbGroup;
		ICommCodeRepository dbCode;

		public CommCodeController() { }

		public CommCodeController(ICodeGroupRepository dbGroup, ICommCodeRepository dbCode)
		{
			this.dbGroup = dbGroup;
			this.dbCode = dbCode;
		}

		// GET: CommCode
		public ActionResult Index()
        {
			List<SelectListItem> groupList = new List<SelectListItem>();
			
			groupList = dbGroup.GetCodeGroups();
			ViewBag.groupList = Utilization.addTopEmptyValue(groupList); ;

			return View();
		}
		
		public ActionResult GetCommCodes(int pageNumber = 1, int pageSize = 15, string selCogdGroup = null)
		{
			try
			{
				var pagedData = Pagination.PagedResult(dbCode.GetCommCodes(selCogdGroup), pageNumber, pageSize);
				return Json(pagedData, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult geCodeGroup()
		{
			List<SelectListItem> groupList = new List<SelectListItem>();

			groupList = dbGroup.GetCodeGroups();

			return Json(groupList, JsonRequestBehavior.AllowGet);
		}

		// GET: Code/Create
		public ActionResult Create()
		{
			return PartialView("Create");
		}

		[HttpGet]
		public ActionResult CodeNew(string group)
		{
			ViewBag.CodeGroupid = group;
			return PartialView("CodeNew");
		}

		public ActionResult CodeGroupNew()
		{
			return PartialView("CodeGroupNew");
		}

		public ActionResult Modal()
		{
			return PartialView("ModalView");
		}

		// POST: Code/Create
		[HttpPost]
		public JsonResult SaveCode(yuriPortal.Data.Models.CommCode codes)
		{
			try
			{
				// TODO: Add insert logic here
				if (ModelState.IsValid) 
				{
					dbCode.SaveCode(codes, System.Web.HttpContext.Current.User.Identity.GetUserId());
				}


				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}

		// POST: Code/Create
		[HttpPost]
		public ActionResult CreateGroup(CodeGroup groups)
		{
			try
			{
				// TODO: Add insert logic here
				if (ModelState.IsValid)
				{
					dbGroup.CreateGroup(groups);

					return Json(new { success = true, responseText = "Code grooup is successfuly saved!" }, JsonRequestBehavior.AllowGet);
					
				}
				else{

					return Json(new { success = false, responseText = "Code group is not saved." }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Content(error);
			}
		}

		// POST: Code/Create
		[HttpPost]
		public JsonResult UpdateCodeStatus(string groupVal, string codeVal, string action)
		{
			try
			{
				dbGroup.UpdateCodeStatus(groupVal, codeVal, action);

				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}


	}
}

