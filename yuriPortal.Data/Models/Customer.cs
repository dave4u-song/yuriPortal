using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuriPortal.Data.Models
{
	[Table("Customer")]
	public partial class Customer
	{
		[Key]
		[StringLength(128)]
		public string CustomerId { get; set; }

		[StringLength(100)]
		public string CustomerName { get; set; }

		[StringLength(500)]
		public string CustEmail { get; set; }

		[StringLength(100)]
		public string CustPhoneNumber { get; set; }

		[StringLength(100)]
		public string CustMobileNumber { get; set; }

		[StringLength(100)]
		public string CustFaxNumber { get; set; }

		[StringLength(50)]
		public string CustomerType { get; set; }

		[StringLength(256)]
		public string Street { get; set; }

		[StringLength(50)]
		public string City { get; set; }

		[StringLength(50)]
		public string Province { get; set; }

		[StringLength(50)]
		public string Country { get; set; }

		[StringLength(50)]
		public string PostalCode { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IsDelete { get; set; }

		[StringLength(128)]
		public string OwnerId { get; set; }

		[StringLength(128)]
		public string CustomerPic { get; set; }

		public DateTime CreateDt { get; set; }

		[StringLength(128)]
		public string CreateId { get; set; }

		public DateTime UpdateDt { get; set; }

		[StringLength(128)]
		public string UpdateId { get; set; }

	}
}
