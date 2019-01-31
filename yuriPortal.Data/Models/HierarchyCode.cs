using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace yuriPortal.Data.Models
{
	[Table("HierarchyCode")]
	public partial class HierarchyCode
	{
		[Key]
		[StringLength(50)]
		public string HrerarchyCd { get; set; }

		[Required]
		[StringLength(100)]
		public string CodeName { get; set; }

		public int HireLevel { get; set; }

		public int OrderNo { get; set; }

		[StringLength(50)]
		public string ParentHrerarchyCd { get; set; }

		public int? IsDelete { get; set; }

		public DateTime CreateDt { get; set; }

		[StringLength(128)]
		public string CreateId { get; set; }

		public DateTime UpdateDt { get; set; }

		[StringLength(128)]
		public string UpdateId { get; set; }
	}
}
