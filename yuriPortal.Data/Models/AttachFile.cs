namespace yuriPortal.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("AttachFile")]
	public partial class AttachFile
	{
		[Key]
		public string FileId { get; set; }

		[StringLength(50)]
		public string FileType { get; set; }

		[StringLength(128)]
		public string OriginFileName { get; set; }

		[StringLength(128)]
		public string SaveAsFileName { get; set; }

		[StringLength(15)]
		public string FileExtension { get; set; }

		[StringLength(1000)]
		public string FilePath { get; set; }

		public int IsDelete { get; set; }

		public DateTime CreateDt { get; set; }

		[StringLength(128)]
		public string CreateId { get; set; }
	}
}
