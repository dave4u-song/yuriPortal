using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuriPortal.Data.Models
{
	[Table("TaskAssigned")]
	public partial class TaskAssigned
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int CustTaskId { get; set; }

		[StringLength(128)]
		public string UserId { get; set; }

		[StringLength(50)]
		public string TaskStatus { get; set; }

		public DateTime AssignedDt { get; set; }
	}
}
