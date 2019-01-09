namespace yuriPortal.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuRole")]
    public partial class MenuRole
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string MenuId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string RoleId { get; set; }
    }
}
