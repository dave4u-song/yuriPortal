using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;
using yuriPortal.Data.ViewModel;

namespace yuriPortal.Core.Repository
{
	public class CustomerRepository : ICustomerRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public List<CustomerViewModel> GetCustomerList(string seachText = null)
		{

			var results = (from cust in context.Customers
						   select new
						   {
							   cust.CustomerId,
							   cust.CustomerName,
							   CustEmail = cust.CustEmail ?? "",
							   CustPhoneNumber = cust.CustPhoneNumber ?? "",
							   CustMobileNumber = cust.CustMobileNumber ?? "",
							   CustFaxNumber = cust.CustFaxNumber ?? "",
							   Street = cust.Street ?? "",
							   Province = cust.Province ?? "",
							   City = cust.City ?? "",
							   Country = cust.Country ?? "",
							   cust.IsDelete,
							   cust.CreateId,
							   cust.CreateDt
						   }).ToList();


			if (!string.IsNullOrEmpty(seachText))
			{
				results = results.Where(w => w.CustomerName.Contains(seachText)).ToList();
			}

			results = results.OrderBy(w => w.CustomerName).ToList();


			var viweModel = results.Select(x => new CustomerViewModel
			{
				CustomerId = x.CustomerId,
				CustomerName = x.CustomerName,
				CustPhoneNumber = x.CustPhoneNumber,
				CustMobileNumber = x.CustMobileNumber,
				CustFaxNumber = x.CustFaxNumber,
				IsDelete = x.IsDelete,
				AddressCity = x.City,
				AddressStreet = x.Street,
				AddressProvince = x.Province,
				AddressCountry = x.Country,
				CustEmail = x.CustEmail,
				CustomerType = ""
			});

			return viweModel.ToList<CustomerViewModel>();
		}

		public CustomerViewModel GetCustomerDetail(string customerId, string defaultImagePath)
		{
			var results = (from cust in context.Customers
						   join pic in context.AttachFiles on cust.CustomerPic equals pic.FileId into photo
						   from custphoto in photo.DefaultIfEmpty()
						   where cust.CustomerId == customerId
						   select new
						   {
							   cust.CustomerId,
							   cust.CustomerName,
							   CustEmail = cust.CustEmail ?? "",
							   CustPhoneNumber = cust.CustPhoneNumber ?? "",
							   CustMobileNumber = cust.CustMobileNumber ?? "",
							   CustFaxNumber = cust.CustFaxNumber ?? "",
							   Street = cust.Street ?? "",
							   Province = cust.Province ?? "",
							   City = cust.City ?? "",
							   Country = cust.Country ?? "",
							   cust.IsDelete,
							   cust.CreateId,
							   cust.CreateDt,
							   CustomerPic = custphoto.FileId == null ? defaultImagePath : custphoto.FilePath + custphoto.SaveAsFileName
						   }).FirstOrDefault();


			var viweModel = new CustomerViewModel
			{
				CustomerId = results.CustomerId,
				CustomerName = results.CustomerName,
				CustPhoneNumber = results.CustPhoneNumber,
				CustMobileNumber = results.CustMobileNumber,
				CustFaxNumber = results.CustFaxNumber,
				IsDelete = results.IsDelete,
				AddressCity = results.City,
				AddressStreet = results.Street,
				AddressProvince = results.Province,
				AddressCountry = results.Country,
				CustEmail = results.CustEmail,
				CustomerType = "",
				CustomerPic = results.CustomerPic
			};


			return viweModel;
		}


		public string SaveCustomer(Customer custs)
		{
			custs.UpdateDt = DateTime.Now;
			custs.CreateDt = DateTime.Now;
			custs.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			custs.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			context.Customers.Add(custs);
			context.SaveChanges();

			return custs.CustomerId;
		}

		public string DuplicateCheck(string CustomerName)
		{
			var result = context.Customers.Where(x => x.CustomerName == CustomerName.Trim()).SingleOrDefault();

			var customerId = "";

			if (result != null)
				customerId = result.CustomerId;

			return customerId;
		}



		public void UpdateCustomer(Customer custInfo)
		{
			var item = context.Customers.Where(x => x.CustomerId == custInfo.CustomerId).Single();

			item.CustomerName		= custInfo.CustomerName;
			item.CustEmail			= custInfo.CustEmail;
			item.CustPhoneNumber	= custInfo.CustPhoneNumber;
			item.CustMobileNumber	= custInfo.CustMobileNumber;

			item.CustFaxNumber		= custInfo.CustFaxNumber;
			item.Street				= custInfo.Street;
			item.Province			= custInfo.Province;
			item.City				= custInfo.City;
			item.Country			= custInfo.Country;

			item.PostalCode			= custInfo.PostalCode;
			item.CustomerType		= custInfo.CustomerType;
			item.Country			= custInfo.Country;
			item.UpdateDt			= DateTime.Now;
			item.UpdateId			= System.Web.HttpContext.Current.User.Identity.GetUserId();

			context.SaveChanges();
		}

		public void SaveCustomerPhoto(string custId, string fileName, string strExt, string strSaveFileName)
		{
			var guid = Guid.NewGuid();

			AttachFile files = new AttachFile();
			files.FileId = guid.ToString();
			files.FileType = "CUSTOMER_LOGO";
			files.OriginFileName = fileName;
			files.FileExtension = strExt;
			files.SaveAsFileName = strSaveFileName;
			files.FilePath = "/Files/Customerpic/";
			files.IsDelete = 0;
			files.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			files.CreateDt = DateTime.Now;

			context.AttachFiles.Add(files);

			//var results = context.Customers.Find(custName);

			var results = context.Customers.Where(x => x.CustomerId == custId);

			foreach (var item in results)
			{
				item.CustomerPic = guid.ToString();
			}

			context.SaveChanges();
		}

		public int SaveNote(CustomerNote noteInfo)
		{
			if (string.IsNullOrEmpty(noteInfo.NoteId.ToString()) || noteInfo.NoteId == 0)
			{
				noteInfo.UpdateDt = DateTime.Now;
				noteInfo.CreateDt = DateTime.Now;
				noteInfo.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
				noteInfo.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

				context.CustomerNotes.Add(noteInfo);
				context.SaveChanges();
			}
			else
			{
				var updateRow = (from note in context.CustomerNotes
								 where note.NoteId == noteInfo.NoteId
								 select note).Single();

				updateRow.NoteContents = noteInfo.NoteContents;
				updateRow.UpdateDt = DateTime.Now;
				updateRow.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			}

			return noteInfo.NoteId; ;
		}

		public List<CustomerNote> GetNoteList(string CustomerId = null)
		{

			var results = (from cust in context.CustomerNotes
						   join urs in context.UserProfiles on cust.CreateId equals urs.UserId into bru
						   from uid in bru.DefaultIfEmpty()
						   //where cust.CustomerId == CustomerId
						   select new
						   {
							   cust.CustomerId,
							   cust.NoteId,
							   NoteStatus = cust.NoteStatus ?? "",
							   cust.NoteContents,
							   cust.CreateId,
							   cust.CreateDt,
							   FullName = uid.FirstName + " " + uid.LastName
						   }).ToList();


			if (!string.IsNullOrEmpty(CustomerId))
			{
				results = results.Where(w => w.CustomerId == CustomerId).ToList();
			}

			results = results.OrderByDescending(w => w.CreateDt).ToList();


			var viweModel = results.Select(x => new CustomerNote
			{
				CustomerId = x.CustomerId,
				NoteId = x.NoteId,
				NoteStatus = x.NoteStatus,
				NoteContents = x.NoteContents,
				CreateId = x.CreateId,
				CreateDt = x.CreateDt,
				FullName = x.FullName
			});

			return viweModel.ToList<CustomerNote>();
		}

		public CustomerNote DetailCustomerNote(int? NoteId){

			var results = (from brd in context.CustomerNotes
						   join urs in context.UserProfiles on brd.CreateId equals urs.UserId into bru
						   from uid in bru.DefaultIfEmpty()
						   where brd.NoteId == NoteId
						   select new
						   {
							   brd.NoteId,
							   brd.CustomerId,
							   brd.NoteContents,
							   brd.NoteStatus,
							   brd.CreateId,
							   brd.CreateDt,
							   FullName = uid.FirstName + " " + uid.LastName
						   }).FirstOrDefault();


			var viweModel = new CustomerNote
			{
				NoteId = results.NoteId,
				CustomerId = results.CustomerId,
				NoteContents = results.NoteContents,
				NoteStatus = results.NoteStatus,
				CreateId = results.CreateId,
				CreateDt = results.CreateDt,
				FullName = results.FullName
			};

			return viweModel;
		}


		public List<dynamic> GetActivityList(string seachText = null)
		{
			var results = from lang in context.CustTasks
						  select new
						  {
							  lang.CustomerId,
							  lang.CustTaskId,
							  lang.TaskSubject,
							  lang.TaskBeginDt,
							  lang.TaskEndDt,
							  lang.TaskStatus,
							  lang.TaskCompleteDt
						  };

	
			if (!string.IsNullOrEmpty(seachText))
			{
				results = results.Where(w => w.TaskSubject.Contains(seachText));
			}

			results = results.OrderByDescending(w => w.CustTaskId);

			return results.ToList<dynamic>();
		}


	}
}
