using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuriPortal.Data.Models
{
	[Table("CustomerNote")]
	public partial class CustomerNote
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NoteId { get; set; }

		[StringLength(128)]
		public string CustomerId { get; set; }

		[StringLength(50)]
		public string NoteStatus { get; set; }

		public string NoteContents { get; set; }

		public DateTime CreateDt { get; set; }

		[StringLength(128)]
		public string CreateId { get; set; }

		public DateTime UpdateDt { get; set; }

		[StringLength(128)]
		public string UpdateId { get; set; }

		[NotMapped]
		public string FullName { get; set; }
	}
}
