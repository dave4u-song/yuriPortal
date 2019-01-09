namespace yuriPortal.Web.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("BoardLike")]
	public partial class BoardLike
	{
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int LikeId { get; set; }

		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int PostId { get; set; }

		[Key]
		[Column(Order = 2)]
		[StringLength(18)]
		public string CreateId { get; set; }

		public DateTime CreateDt { get; set; }

		public int IsLike { get; set; }

		public virtual BoardPost BoardPost { get; set; }
	}
}
