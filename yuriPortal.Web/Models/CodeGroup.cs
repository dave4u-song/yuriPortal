namespace yuriPortal.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CodeGroup")]
    public partial class CodeGroup
    {
        [StringLength(15)]
        public string CodeGroupID { get; set; }

		[StringLength(100)]
		public string CodeGroupText { get; set; }

		[StringLength(1000)]
        public string Description { get; set; }

        public DateTime CreateDt { get; set; }

        [StringLength(128)]
        public string CreateId { get; set; }

        public DateTime UpdateDt { get; set; }

        [StringLength(128)]
        public string UpdateId { get; set; }
    }
}
