using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace yuriPortal.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

		public DbSet<Menu> Menus { get; set; }
		public DbSet<AppPath> AppPaths { get; set; }
		public DbSet<CommCode> CommCodes { get; set; }
		public DbSet<CodeGroup> CodeGroups { get; set; }
		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<AttachFile> AttachFiles { get; set; }
		public DbSet<Board> Boards { get; set; }
		public DbSet<BoardPost> BoardPosts { get; set; }
		public DbSet<BoardComment> BoardComments { get; set; }
		public DbSet<BoardLike> BoardLikes { get; set; }
		public DbSet<BoardAttach> BoardAttachs { get; set; }
		public DbSet<Language> Languages { get; set; }

		//static ApplicationDbContext()
  //      {
  //          // Set the database intializer which is run once during application start
  //          // This seeds the database with admin user credentials and admin role
  //          Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
  //      }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ApplicationUser>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
			modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
			modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
			modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimId"); ;
			modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId"); ;

			//modelBuilder.Entity<Menu>().HasMany(e => e.Children).WithOptional().HasForeignKey(e => e.ParentMenuId);
		}

		//public System.Data.Entity.DbSet<yuriPortal.Web.Models.ApplicationUser> ApplicationUsers { get; set; }
	}
}