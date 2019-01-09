using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using yuriPortal.Data.Models;

namespace yuriPortal.Web.ViewModel
{
	public class UserDetailViewModel
	{
		public ApplicationUser User { get; set; }
		public UserProfile UserProfile { get; set; }
	}

	public class UserSaveViewModel
	{
		[Key]
		[Required(AllowEmptyStrings = false)]
		public string UserId { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "User Name")]
		public string UserName { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		
		[Display(Name = "Phone Number")]
		[Phone]
		public string PhoneNumber { get; set; }

		[Display(Name = "Deleted")]
		public int? IsDelete { get; set; }

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

		[Display(Name = "Gender")]
		public string Gender { get; set; }

		[Display(Name = "Language")]
		public string Language { get; set; }

		[Display(Name = "Language")]
		public string UserPic { get; set; }

		//public IEnumerable<UserProfile> userProfile { get; set; }
	}
}