using System;
using Microsoft.AspNet.Identity;
using System.Resources;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using yuriPortal.Core;
using yuriPortal.Data.Models;
using yuriPortal.Web.Helper;
using yuriPortal.Core.Interfaces;

namespace yuriPortal.Web.Controllers
{
    public class LangController : Controller
    {
		ILangRepository dbLang;

		public LangController() { }

		public LangController(ILangRepository dbLang)
		{
			this.dbLang = dbLang;
		}

		// GET: Lang
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult GetLangList(int pageNumber = 1, int pageSize = 15, string langCode = null, string seachText = null)
		{

			try
			{
				List<dynamic> lists = new List<dynamic>();
				lists = dbLang.GetLangList(langCode, seachText);

				var pagedData = Pagination.PagedResult(lists, pageNumber, pageSize);
				return Json(pagedData, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult CreateLang()
		{
			ViewBag.UpdateLang = false;
			return PartialView("CreateLang");
		}

		public ActionResult ViewContent(string LangId)
		{
			ViewBag.UpdateLang = true;
			return PartialView("CreateLang", dbLang.ViewContent(LangId));
		}


		[HttpPost]
		public ActionResult SaveLang(Language langs)
		{
			try
			{
				if (ModelState.IsValid)
				{
					dbLang.SaveLang(langs);

					return Json(new { success = true, responseText = "The message is successfuly saved!" }, JsonRequestBehavior.AllowGet);

				}
				else
				{

					return Json(new { success = false, responseText = "The message is not saved." }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, responseText = error }, JsonRequestBehavior.AllowGet);
			}
		}

		// POST: Code/Create
		[HttpPost]
		public ActionResult UpdateLang(Language langs)
		{
			try
			{
				if (ModelState.IsValid)
				{
					dbLang.UpdateLang(langs);

					return Json(new { success = true, responseText = "Language is successfuly saved!" }, JsonRequestBehavior.AllowGet);
				}
				else
				{

					return Json(new { success = false, responseText = "Language is not saved." }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, responseText = "Unable to save changes. Try again, and if the problem persists, see your system administrator." }, JsonRequestBehavior.AllowGet);
			}
		}


		public ActionResult DeployResource(string langCode)
		{
			try
			{
				var results = dbLang.GetLangList(langCode, null);

				List<Language> lists = new List<Language>();

				foreach (var ln in results){
					Language lang = new Language();

					lang.LangId = ln.LangId;
					lang.LangTrans = ln.LangTrans;
					lang.DefaultText = ln.DefaultText;
					lists.Add(lang);
				}

				CreateReource(lists, langCode);

				return Json(new { success = true, responseText = "Deploy is successfuly done!" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SresponseText = error }, JsonRequestBehavior.AllowGet);
			}
		}

		public void CreateReource(List<Language> list, string langCode)
		{
			try
			{
				// Create a instance of ResourceWriter and specify the name of the resource file.
				System.Resources.ResourceWriter RWObj = new ResourceWriter(AppDomain.CurrentDomain.BaseDirectory + "/Resources/LanguageResource." + langCode + ".resx");

				for(int i = 0;i < list.Count; i++){
					RWObj.AddResource(list[i].LangId, list[i].LangTrans);
				}

				RWObj.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}