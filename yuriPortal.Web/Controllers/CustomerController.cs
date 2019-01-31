using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;
using yuriPortal.Data.ViewModel;
using yuriPortal.Helper;
using yuriPortal.Web.Helper;

namespace yuriPortal.Web.Controllers
{
    public class CustomerController : Controller
    {
		ICustomerRepository dbCust;

		public CustomerController() { }

		public CustomerController(ICustomerRepository dbCust)
		{
			this.dbCust = dbCust;
		}

		// GET: Customer
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult GetCustomerList(int pageNumber = 1, int pageSize = 15, string seachText = null, string seachCondition = null)
		{
			try
			{
				List<CustomerViewModel> posts = new List<CustomerViewModel>();

				posts = dbCust.GetCustomerList(seachText);

				var pagedData = Pagination.PagedResult(posts, pageNumber, pageSize);
				return Json(pagedData, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult CustomerDetail(string customerId){
			if (customerId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var defaultImagePath = ConfigurationManager.AppSettings["defaultLogoPath"];

			List<CustomerNote> notes = new List<CustomerNote>();

			notes = dbCust.GetNoteList(customerId);


			ViewBag.getNoteList = notes;

			return PartialView("CustomerDetail", dbCust.GetCustomerDetail(customerId, defaultImagePath));
		}

		public ActionResult CustomerReview()
		{
			return PartialView("CustomerReview");
		}

		
		public ActionResult CustomerCreate()
		{
			return PartialView("CustomerCreate");
		}

		public ActionResult CustomerInitial(){
			
			return Content("string value");
		}

		public ActionResult SaveCustomer(Customer cust){

			string strCustomerId = dbCust.DuplicateCheck(cust.CustomerName);
			if (!string.IsNullOrEmpty(strCustomerId))
			{
				return Json(new { success = false, responseText = strCustomerId }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				var guid = Guid.NewGuid();

				cust.CustomerId = guid.ToString();


				return Json(dbCust.SaveCustomer(cust), JsonRequestBehavior.AllowGet);
			}
		}


		public JsonResult UpdateCustomer(Customer custInfo)
		{

			try
			{
				// TODO: Add insert logic here
				dbCust.UpdateCustomer(custInfo);

				return Json(new { success = true, responseText = "The menu is successfuly updated!" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}


		[HttpPost]
		[AllowAnonymous]
		public ActionResult SaveCustomerPhoto(string custId)
		{
			FileManager fm = new FileManager();
			HttpPostedFileBase file = Request.Files[0];
			string strSaveFileName = "";
			string customerpicPath = ConfigurationManager.AppSettings["customerpicPath"];

			try
			{
				if (file.ContentLength > 0)
				{
					var fileName = Path.GetFileName(file.FileName);
					strSaveFileName = fm.SaveFile(file, customerpicPath);

					var guid = Guid.NewGuid();

					dbCust.SaveCustomerPhoto(custId, fileName, Path.GetExtension(fileName), strSaveFileName);
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

		public ActionResult ManageNote(string customerId = null)
		{

			return PartialView("ManageNote");
		}


		public ActionResult SaveCustomerNote(CustomerNote noteInfo)
		{
			if (ModelState.IsValid)
			{
				int notetId = dbCust.SaveNote(noteInfo);

				return Json(dbCust.DetailCustomerNote(notetId), JsonRequestBehavior.AllowGet);
			}
			else
			{

				return Json(new { success = false, SaveError = "Not Saved" }, JsonRequestBehavior.AllowGet);
			}

		}

		public ActionResult GetNoteList(string CustomerId)
		{
			try
			{
				List<CustomerNote> notes = new List<CustomerNote>();

				notes = dbCust.GetNoteList(CustomerId);

				return Json(notes, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}


		public ActionResult ManageDeal(string customerId = null)
		{

			return PartialView("ManageDeal");
		}

		public ActionResult ManageActivity(string customerId = null)
		{

			return PartialView("ManageActivity");
		}

		public ActionResult ManagePurchase(string customerId = null)
		{

			return PartialView("ManagePurchase");
		}

		public ActionResult ManageInvoice(string customerId = null)
		{

			return PartialView("ManageInvoice");
		}

		public ActionResult ManageEdit(string customerId = null)
		{
			var defaultImagePath = ConfigurationManager.AppSettings["defaultLogoPath"];

			return PartialView("ManageEdit", dbCust.GetCustomerDetail(customerId, defaultImagePath));
		}



		public ActionResult GetNoteListForCal(string CustomerId = null)
		{
			try
			{
				List<CustomerNote> notes = new List<CustomerNote>();
				List<FullCalendarData> evList = new List<FullCalendarData>();

				notes = dbCust.GetNoteList(CustomerId);

				for (int i = 0; i < notes.Count; i++)
				{
					FullCalendarData calData = new FullCalendarData();

					//DateTime startTime = DateTime.ParseExact(notes[i].CreateDt.ToLongDateString(), "yyyy-MM-dd'T'HH:mm:ss:ffffff'Z'zz", CultureInfo.InvariantCulture, DateTimeStyles.None);

					calData.id = notes[i].NoteId.ToString();

					var strTitle = "";

					if (notes[i].NoteContents.Length >= 10)
						strTitle = notes[i].NoteContents.Substring(0, 10) + "...";
					else
						strTitle = notes[i].NoteContents;

					calData.title = strTitle;
					calData.color = "red";
					calData.type = "NOTE";
					calData.start = notes[i].CreateDt.ToString();


					evList.Add(calData);
				}


				return Json(evList, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}


		public ActionResult GetActivityList(int pageNumber = 1, int pageSize = 15, string seachText = null)
		{

			try
			{
				List<dynamic> lists = new List<dynamic>();
				lists = dbCust.GetActivityList(seachText);

				var pagedData = Pagination.PagedResult(lists, pageNumber, pageSize);
				return Json(pagedData, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}


		public ActionResult CreateActivity()
		{
			ViewBag.UpdateActivity = false;
			return PartialView("CreateActivity");
		}



	}
}