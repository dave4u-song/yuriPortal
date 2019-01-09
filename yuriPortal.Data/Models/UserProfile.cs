namespace yuriPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserProfile")]
    public partial class UserProfile
    {
        [Key]
        public string UserId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

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

		[StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string LanguageCd { get; set; }

		[StringLength(128)]
		public string UserPic { get; set; }
    }
}
