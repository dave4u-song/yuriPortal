namespace yuriPortal.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("BoardPost")]
	public partial class BoardPost
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public BoardPost()
		{
			BoardComments = new HashSet<BoardComment>();
			BoardLikes = new HashSet<BoardLike>();
			PostAttachs = new HashSet<BoardAttach>();
		}

		public int Board_id { get; set; }

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PostId { get; set; }

		[StringLength(500)]
		public string PostSubject { get; set; }

		public string PostContent { get; set; }

		//public int IsDelete { get; set; }

		[StringLength(50)]
		public string Status { get; set; }

		public DateTime CreateDt { get; set; }

		[StringLength(128)]
		public string CreateId { get; set; }

		public DateTime UpdateDt { get; set; }

		[StringLength(128)]
		public string UpdateId { get; set; }

		public virtual Board Board { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<BoardComment> BoardComments { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<BoardLike> BoardLikes { get; set; }


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<BoardAttach> PostAttachs { get; set; }

		[NotMapped]
		public string[] PostAttach { get; set; }

		[NotMapped]
		public int LikeCount { get; set; }

		[NotMapped]
		public int DisLikeCount { get; set; }
	}
}
