using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using yuriPortal.Data.Models;

namespace yuriPortal.Data.ViewModel
{
	public class MenuViewModel
	{
		[Required(AllowEmptyStrings = false)]
		public string MenuId { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string MenuName { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string ParentMenuId { get; set; }

		[Required(AllowEmptyStrings = false)]
		public int MenuLevel { get; set; }

		public int OrderNo { get; set; }

		public int IsDelete { get; set; }

		public int ChildCnt { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "User Full Name")]
		public string FullName { get; set; }

		[Display(Name = "Create Date")]
		public DateTime CreateDt { get; set; }

		[Display(Name = "Create Usesr")]
		public string CreateId { get; set; }

		[Display(Name = "Create Date")]
		public DateTime UpdateDt { get; set; }

		[Display(Name = "Create Usesr")]
		public string UpdateId { get; set; }

		public string AppPathId { get; set; }

		public string PagePath { get; set; }
		
	}
}
