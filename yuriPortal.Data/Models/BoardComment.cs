namespace yuriPortal.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("BoardComment")]
	public partial class BoardComment
	{
		public int PostId { get; set; }

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CommetId { get; set; }

		public int CommentLevel { get; set; }

		public int? ParentId { get; set; }

		public string CommentContent { get; set; }

		public DateTime CreateDt { get; set; }

		[StringLength(128)]
		public string CreateId { get; set; }

		public DateTime UpdateDt { get; set; }

		[StringLength(128)]
		public string UpdateId { get; set; }

		public virtual BoardPost BoardPost { get; set; }

		[NotMapped]
		public string FullName { get; set; }

		[NotMapped]
		public string UserPic { get; set; }

		[NotMapped]
		public List<BoardComment> Children { get; set; }
	}
}
