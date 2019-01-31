using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using yuriPortal.Data.Models;

namespace yuriPortal.Data.ViewModel
{
	public class CustomerViewModel
	{
		[Required(AllowEmptyStrings = false)]
		public string CustomerId { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Customer Name")]
		public string CustomerName { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Email")]
		[EmailAddress]
		public string CustEmail { get; set; }

		[Display(Name = "Phone Number")]
		[Phone]
		public string CustPhoneNumber { get; set; }

		[Display(Name = "Phone Number")]
		[Phone]
		public string CustMobileNumber { get; set; }

		[Display(Name = "Phone Number")]
		[Phone]
		public string CustFaxNumber { get; set; }

		[Display(Name = "Deleted")]
		public int? IsDelete { get; set; }

		[Display(Name = "Customer Type")]
		public string CustomerType{ get; set; }

		[Display(Name = "Street")]
		public string AddressStreet { get; set; }

		[Display(Name = "City")]
		public string AddressCity { get; set; }

		[Display(Name = "Province")]
		public string AddressProvince { get; set; }

		[Display(Name = "Country")]
		public string AddressCountry { get; set; }

		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }

		public string OwnerId { get; set; }

		[Display(Name = "Owner Name")]
		public string OwnerName { get; set; }

		[Display(Name = "Customer Pic")]
		public string CustomerPic { get; set; }
	}
}
