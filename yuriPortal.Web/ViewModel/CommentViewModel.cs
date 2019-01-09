using System;
using System.ComponentModel.DataAnnotations;

namespace yuriPortal.Data.ViewModel
{
	public class CommentViewModel
	{
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