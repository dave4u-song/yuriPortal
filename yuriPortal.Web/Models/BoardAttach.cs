namespace yuriPortal.Web.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("BoardAttach")]
	public partial class BoardAttach
	{
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int PostId { get; set; }

		[Key]
		[Column(Order = 1)]
		public string FileId { get; set; }
	}
}
