using System;
using System.ComponentModel.DataAnnotations;

namespace yuriPortal.Data.ViewModel
{
	public class PostViewModel
	{
		
		[Required(AllowEmptyStrings = false)]
		public int Board_id { get; set; }

		[Key]
		[Required(AllowEmptyStrings = false)]
		public int PostId { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "User Photo")]
		public string UserPhoto { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "User Full Name")]
		public string UserFullName { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Subject")]
		public string PostSubject { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Content")]
		public string PostContent { get; set; }

		[Required(AllowEmptyStrings = false)]
		[Display(Name = "Status")]
		public string Status { get; set; }

		[Display(Name = "Attachment")]
		public int? IsAttach { get; set; }

		[Display(Name = "Create Date")]
		public DateTime CreateDt { get; set; }
	}
}