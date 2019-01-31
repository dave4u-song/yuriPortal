namespace yuriPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppPath")]
    public partial class AppPath
    {
		[Key]
        [StringLength(128)]
        public string AppPathId { get; set; }

        [Required]
        [StringLength(100)]
        public string AppPathName { get; set; }

        [StringLength(500)]
        public string PagePath { get; set; }

        [StringLength(500)]
        public string Parameters { get; set; }

		[StringLength(1000)]
		public string Description { get; set; }

		public int IsDelete { get; set; }

        public DateTime CreateDt { get; set; }

        [StringLength(128)]
        public string CreateId { get; set; }

        public DateTime UpdateDt { get; set; }

        [StringLength(128)]
        public string UpdateId { get; set; }

		[NotMapped]
		public string FullUserName { get; set; }
	}
}
