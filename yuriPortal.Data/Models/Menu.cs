namespace yuriPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [StringLength(50)]
        public string MenuId { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuName { get; set; }

        public int MenuLevel { get; set; }

		public int OrderNo { get; set; }

		[StringLength(50)]
        public string ParentMenuId { get; set; }

        [StringLength(128)]
        public string AppPathId { get; set; }

        public int? IsDelete { get; set; }

        public DateTime CreateDt { get; set; }

        [StringLength(128)]
        public string CreateId { get; set; }

        public DateTime UpdateDt { get; set; }

        [StringLength(128)]
        public string UpdateId { get; set; }

		public ICollection<Menu> Children { get; set; }
	}
}
