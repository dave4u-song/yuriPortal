using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using yuriPortal.Data.Models;

namespace yuriPortal.Web.ViewModel
{
	public class CommentViewModel
	{
		//CommetId = x.CommetId,
		//					PostId = x.PostId,
		//					CommentLevel = x.CommentLevel,
		//					ParentId = x.ParentId,
		//					CommentContent = x.CommentContent,
		//					CreateDt = x.CreateDt,
		//					CreateId = x.CreateId,
		//					UpdateDt = x.UpdateDt,
		//					UpdateId = x.UpdateId,
		//					FullName = x.FullName,
		//					UserPic = x.UserPic

		[Required(AllowEmptyStrings = false)]
		public int CommetId { get; set; }

		[Required(AllowEmptyStrings = false)]
		public int PostId { get; set; }

		[Required(AllowEmptyStrings = false)]
		public int ParentId { get; set; }

		[Required(AllowEmptyStrings = false)]
		public int CommentLevel { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Comment")]
		public string CommentContent { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "User Full Name")]
		public string FullName { get; set; }

		[Required(AllowEmptyStrings = false)]
		public string UserPic { get; set; }

		[Display(Name = "Create Date")]
		public DateTime CreateDt { get; set; }

		[Display(Name = "Create Usesr")]
		public string CreateId { get; set; }

		[Display(Name = "Create Date")]
		public DateTime UpdateDt { get; set; }

		[Display(Name = "Create Usesr")]
		public string UpdateId { get; set; }



	}
}