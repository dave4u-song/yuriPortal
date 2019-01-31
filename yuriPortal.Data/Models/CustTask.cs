using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuriPortal.Data.Models
{
	[Table("CustTask")]
	public partial class CustTask
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CustTaskId { get; set; }

		[StringLength(128)]
		public string CustomerId { get; set; }

		[StringLength(50)]
		public string TaskType { get; set; }

		[StringLength(1000)]
		public string TaskSubject { get; set; }

		public string TaskContent { get; set; }

		[StringLength(50)]
		public string TaskStatus { get; set; }

		public DateTime TaskBeginDt { get; set; }

		public DateTime TaskEndDt { get; set; }


		public int DurationMin { get; set; }

		public DateTime TaskCompleteDt { get; set; }

		public DateTime CreateDt { get; set; }

		[StringLength(128)]
		public string CreateId { get; set; }

		public DateTime UpdateDt { get; set; }

		[StringLength(128)]
		public string UpdateId { get; set; }
	}
}
