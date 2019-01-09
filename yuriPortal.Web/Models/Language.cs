namespace yuriPortal.Web.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("Language")]
	public partial class Language
	{
		[Key]
		[Column(Order = 0)]
		[StringLength(50)]
		public string LangId { get; set; }

		[Required]
		public string DefaultText { get; set; }

		[Required]
		public string DefaultTrans { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(50)]
		public string LangCode { get; set; }

		public string LangTrans { get; set; }

		public DateTime CreateDt { get; set; }

		[StringLength(128)]
		public string CreateId { get; set; }

		public DateTime UpdateDt { get; set; }

		[StringLength(128)]
		public string UpdateId { get; set; }
	}
}
