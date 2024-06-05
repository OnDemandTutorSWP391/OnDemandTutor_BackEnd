using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext()
        {
            
        }

        public MyDbContext(DbContextOptions<MyDbContext> option) : base(option) 
        {
            
        }

        #region DbSet
        public virtual DbSet<User> Users {  get; set; }
        public virtual DbSet<IdentityUserToken<string>> UserTokens { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestCategory> RequestCategories { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<SubjectLevel> SubjectLevels { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Time> Times { get; set; }
        public virtual DbSet<StudentJoin> StudentJoins { get; set; }
        public virtual DbSet<CoinManagement> CoinManagements { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("OnDemandTutor"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>(entity => { entity.ToTable(name: "User"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable(name: "UserRoles"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Role"); });

            //auto add role for DB
            //SeedRoles(modelBuilder);

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable(name: "UserClaim"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable(name: "UserLogin"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable(name: "UserToken"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable(name: "RoleClaim"); });
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN"},
                    new IdentityRole() { Name = "Moderator", ConcurrencyStamp = "2", NormalizedName = "MODERATOR" },
                    new IdentityRole() { Name = "Tutor", ConcurrencyStamp = "3", NormalizedName = "TUTOR" },
                    new IdentityRole() { Name = "Student", ConcurrencyStamp = "4", NormalizedName = "STUDENT" }
                );
        }
    }
}
