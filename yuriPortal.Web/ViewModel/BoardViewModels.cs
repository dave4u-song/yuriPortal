using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using yuriPortal.Data.Models;

namespace yuriPortal.Data.ViewModel
{
	public class BoardViewModels
	{
		[Key]
		[Required(AllowEmptyStrings = false)]
		public int Board_id { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Board Name")]
		public string Board_name { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Description")]
		public string Description { get; set; }

		[Display(Name = "Attachment")]
		public int? IsAttach { get; set; }

		[Display(Name = "Comment")]
		public int? IsComment { get; set; }

		[Display(Name = "Like")]
		public int? IsLike { get; set; }

		[Display(Name = "Deleted")]
		public int? IsDelete { get; set; }

		[Display(Name = "Default UI")]
		public string DefaultUI { get; set; }

	}
}