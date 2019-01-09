namespace yuriPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommCode")]
    public partial class CommCode
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Code_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Code_Name { get; set; }

		[Key]
		[Column(Order = 2)]
		[StringLength(15)]
		public string CodeGroupID { get; set; }

		[StringLength(50)]
        public string Code_Value { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? IsDelete { get; set; }

        public DateTime? CreateDt { get; set; }

        [StringLength(128)]
        public string CreateId { get; set; }

        public DateTime? UpdateDt { get; set; }

        [StringLength(128)]
        public string UpdateId { get; set; }
    }
}
