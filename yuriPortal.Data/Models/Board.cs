namespace yuriPortal.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("Board")]
	public partial class Board
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Board()
		{
			BoardPosts = new HashSet<BoardPost>();
		}

		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Board_id { get; set; }

		[Column(Order = 1)]
		[StringLength(50)]
		public string Board_name { get; set; }

		[StringLength(1000)]
		public string Description { get; set; }

		[Column(Order = 2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IsAttach { get; set; }

		[Column(Order = 3)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IsComment { get; set; }

		[Column(Order = 4)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IsLike { get; set; }

		[Column(Order = 5)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int IsDelete { get; set; }

		[StringLength(6)]
		public string DefaultUI { get; set; }

		[Column(Order = 7)]
		public DateTime CreateDt { get; set; }

		[Column(Order = 8)]
		[StringLength(128)]
		public string CreateId { get; set; }

		[Column(Order = 9)]
		public DateTime UpdateDt { get; set; }

		[Column(Order = 10)]
		[StringLength(128)]
		public string UpdateId { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<BoardPost> BoardPosts { get; set; }
	}
}
